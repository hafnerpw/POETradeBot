using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using System.Text.Json;
using System.Threading;
using System.Timers;
using Newtonsoft.Json.Linq;
using POETradeBot.Properties;

namespace POETradeBot
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string Url = "https://www.pathofexile.com/api/trade/data/leagues";
        private const string UrlItemData = "https://www.pathofexile.com/api/trade/data/items";
        private const string UrlTradeSearch = "https://www.pathofexile.com/api/trade/search/";
        private const string UrlTradeFetch = "https://www.pathofexile.com/api/trade/fetch/";
        private const string NormalTrade = "https://www.pathofexile.com/trade/search/";
        private const int Repeat = 10;
        private const int RepeatTrade = 17;
        private int _count;
        private readonly List<Entry> _itemData = new List<Entry>();
        private Entry _selectedEntry;
        private List<TradeListing> _tradesFound;
        private int _currentTradeIndex;

        public Form1()
        {
            Client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
            InitializeComponent();
            GetItemData();
            GetCurrentLeagues();
        }

        public string TradeFiltersFromId(string id)
        {
            var response = Client.GetStringAsync(NormalTrade + comboBox1.Text + "/" + id).Result;
            var i = response.IndexOf("\"state\":");
            if (i != -1)
            {
                response = response.Substring(i);
                var j = response.IndexOf("\"loggedIn\"");
                if (j != -1)
                {
                    response = response.Substring(0, j - 1);
                    response = response.Replace("state", "query");
                    return "{" + response + ",\"sort\":{\"price\":\"asc\"}" + "}";
                }
            }
            return "";
        }

        private void GetItemData()
        {
            var response = Client.GetStringAsync(UrlItemData).Result;
            JsonSerializer.Deserialize<ItemDataArray>(response)?.result.ForEach(result =>
            {
                result.entries.ForEach(entry => _itemData.Add(entry));
            });
        }

        private void GetCurrentLeagues()
        {
            var response = Client.GetStringAsync(Url).Result;
            JsonSerializer.Deserialize<LeagueResult>(response)?.result
                .ForEach(league => comboBox1.Items.Add(league.text));
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;
            textBox1.Enabled = true;
        }

        private bool FormValid()
        {
            return string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(comboBox1.Text) ||
                   !_selectedEntry.name.Contains(textBox1.Text) || !_selectedEntry.type.Contains(comboBox2.Text) ||
                   string.IsNullOrWhiteSpace(_selectedEntry?.name) || string.IsNullOrWhiteSpace(_selectedEntry.type)
                   || string.IsNullOrWhiteSpace(_selectedEntry.text);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (FormValid())
            {
                MessageBox.Show(@"geht nicht");
                return;
            }

            var itemToSearch = CreateSearchQuery(textBox1.Text, comboBox2.Text);

            var content = new StringContent(itemToSearch, Encoding.UTF8, "application/json");
            var result = Client.PostAsync(UrlTradeSearch + comboBox1.Text, content);

            var response = await result.Result.Content.ReadAsStringAsync();
            if ((int)JObject.Parse(response)["total"] == 0)
            {
                label2.Text = "No Items found";
                ClearForm();
                return;
            }

            var searchId = JObject.Parse(response)["id"]?.ToString();

            var list = JObject.Parse(response)["result"]?.Select(c => (string)c).ToList();
            var first10 = list?.Take(10).Aggregate((a, b) => a + "," + b);


            var items = await Client.GetStringAsync(UrlTradeFetch + first10 + "?query=" + searchId);
            _tradesFound = JObject.Parse(items)["result"]?
                .Select(c => JsonSerializer.Deserialize<TradeListing>(c.ToString())).ToList();
            if (_tradesFound == null || _tradesFound.Count == 0) return;
            var avgPrice = _tradesFound.Average(c => c.listing.price.amount);
            _tradesFound.ForEach(lsItem => { listBox1.Items.Add(lsItem); });

            label2.Text = $@"First {_tradesFound.Count} items for {textBox1.Text}. ~Price: {avgPrice}";
        }

        private static string CreateSearchQuery(string name, string type = "", int minPrice = 1, int maxPrice = 30)
        {
            var rtnString =
                @"{""query"":{""status"":{""option"":""online""},""name"":""Tabula Rasa"",""type"":""Simple Robe"",""stats"":[{""type"":""and""}],""filters"":{""trade_filters"":{""filters"":{""price"":{""min"":1,""max"":20}}},""misc_filters"":{""filters"":{""corrupted"":{""option"":""false""}}}}},""sort"":{""price"":""asc""}}";
            rtnString = rtnString.Replace("Tabula Rasa", name);
            rtnString = rtnString.Replace("Simple Robe", type);
            rtnString = rtnString.Replace("\"min\": 1", "\"min\": " + minPrice);
            rtnString = rtnString.Replace("\"max\": 1", "\"max\": " + maxPrice);
            rtnString = rtnString.Replace("\"", "\"");
            return rtnString;
        }

        private void ClearForm()
        {
            _selectedEntry = null;
            textBox1.Text = "";
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            listBox1.Items.Clear();
            label3.Text = "";
            button1.Enabled = false;
            textboxId.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 3)
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
                return;
            }

            if (textBox1.Text == _selectedEntry?.name) return;
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            var entriesThatFit = _itemData.FindAll(a => a.text.Contains(textBox1.Text));
            if (entriesThatFit.Count == 0)
            {
                comboBox2.Enabled = false;
                return;
            }

            comboBox2.Enabled = true;
            var types = entriesThatFit.Select(a => a.type).ToArray();
            comboBox2.Items.AddRange(types);
            if (entriesThatFit.Count == 1) comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedEntry = _itemData.FindAll(a => a.text.Contains(textBox1.Text))
                .FirstOrDefault(a => a.type == comboBox2.Text);
            if (_selectedEntry == null) return;
            textBox1.Text = _selectedEntry?.name;
            label3.Text = _selectedEntry == null ? "No Item found" : $"{_selectedEntry.name} ({_selectedEntry.type})";
            button1.Enabled = true;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _count = 0;
            waitForInvTimer.Stop();
            waitForTrade.Stop();
            var index = listBox1.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches) return;
            _currentTradeIndex = index;
            BotUtils.SendKeysToPoe(_tradesFound[_currentTradeIndex].listing.whisper);
            waitForInvTimer.Start();
            listBox1.Enabled = false;
        }

        private bool CheckForInvite()
        {
            var image1 = new Bitmap(Resources.party_invite);
            image1 = BotUtils.ConvertToFormat(image1);
            pattern.Image = image1;

            var beforeFormatting = BotUtils.CaptureScreen(1686, 660, 165, 15);
            var screen = BotUtils.ConvertToFormat(beforeFormatting);
            screenshot.Image = screen;

            return BotUtils.InPicture(screen, image1);
        }

        private bool CheckForTrade()
        {
            var patternImage = new Bitmap(Resources.trade_request);
            patternImage = BotUtils.ConvertToFormat(patternImage);
            patternLabel.Image = patternImage;

            var beforeFormatting = BotUtils.CaptureScreen(1686, 660, 180, 17);
            var screen = BotUtils.ConvertToFormat(beforeFormatting);
            screenshot.Image = screen;

            return BotUtils.InPicture(screen, patternImage);
        }

        private void WaitForPartyInvite(object sender, ElapsedEventArgs e)
        {
            if (_count < RepeatTrade)
            {
                if (!CheckForInvite())
                    _count++;
                else
                {
                    _count = 0;
                    waitForInvTimer.Stop();
                    var characterName = _tradesFound[_currentTradeIndex].listing.whisper.Split(' ')[0].Replace("@", "");
                    BotUtils.ClickAt(1600, 785);
                    Thread.Sleep(2000);
                    BotUtils.SendKeysToPoe("/hideout " + characterName);
                    Thread.Sleep(10000);

                    waitForTrade.Start();
                    listBox1.Enabled = false;
                }
            }
            else
            {
                screenshot.Image = null;
                _count = 0;
                waitForInvTimer.Stop();
                listBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var searchId = textboxId.Text;
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(searchId.Split('/')[5]);

            if (searchId.Contains("/"))
            {
                searchId = searchId.Split('/')[6];
            }

            var tradeFilters = TradeFiltersFromId(searchId);
            listBox1.Items.Clear();
            if (string.IsNullOrWhiteSpace(tradeFilters)) return;


            var content = new StringContent(tradeFilters, Encoding.UTF8, "application/json");
            var result = Client.PostAsync(UrlTradeSearch + comboBox1.Text, content);

            var response = result.Result.Content.ReadAsStringAsync().Result;
            if ((int)JObject.Parse(response)["total"] == 0)
            {
                label2.Text = "No Items found";
                ClearForm();
                return;
            }

            var list = JObject.Parse(response)["result"]?.Select(c => (string)c).ToList();
            var first10 = list?.Take(10).Aggregate((a, b) => a + "," + b);


            var items = Client.GetStringAsync(UrlTradeFetch + first10 + "?query=" + searchId).Result;
            _tradesFound = JObject.Parse(items)["result"]?
                .Select(c => JsonSerializer.Deserialize<TradeListing>(c.ToString())).ToList();
            if (_tradesFound == null || _tradesFound.Count == 0) return;
            var avgPrice = _tradesFound.Average(c => c.listing.price.amount);
            _tradesFound.ForEach(lsItem => { listBox1.Items.Add(lsItem); });

            label2.Text = $@"First {_tradesFound.Count} items for {textBox1.Text}. ~Price: {avgPrice}";
        }

        private void waitForTrade_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_count < Repeat)
            {
                if (!CheckForTrade())
                    _count++;
                else
                {
                    _count = 0;
                    waitForTrade.Stop();
                    BotUtils.ClickAt(1600, 785);
                    Thread.Sleep(1000);
                    BotUtils.SendKeysToPoe("/kick " + textBox2.Text);
                    listBox1.Enabled = true;
                    screenshot.Image = null;
                }
            }
            else
            {
                screenshot.Image = null;
                _count = 0;
                waitForTrade.Stop();
                listBox1.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = Settings.Default.ownCharacter;
            textboxId.Text = Settings.Default.lastUrl;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.ownCharacter = textBox2.Text;
            Settings.Default.lastUrl = textboxId.Text;
            Settings.Default.Save();
        }
    }
}
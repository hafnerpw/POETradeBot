using System.Collections.Generic;

namespace POETradeBot
{
    public class Entry
    {
        public string name { get; set; }
        public string type { get; set; }
        public string text { get; set; }
    }

    public class Result
    {
        public List<Entry> entries { get; set; }
    }

    public class ItemDataArray
    {
        public List<Result> result { get; set; }
    }
}
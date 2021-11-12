using System.Collections.Generic;

namespace POETradeBot
{
    public class Price
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class Online
    {
        public string league { get; set; }
        public string status { get; set; }
    }
    public class Account
    {
        public string name { get; set; }
        public string lastCharacterName { get; set; }
        public Online online { get; set; }
    }

    public class Listing
    {
        public string whisper { get; set; }
        public Price price { get; set; }
        public Account account { get; set; }
    }

    public class TradeListing
    {
        public string id { get; set; }
        public Listing listing { get; set; }
        
        public override string ToString()
        {
            if (listing.account.online.status == "afk")
            {
                return listing.account.name + " " + listing.price.amount + " " + listing.price.currency + " - AFK";
            }
            return listing.account.name + " " + listing.price.amount + " " + listing.price.currency;
        }  

    }
    
    public class League
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class LeagueResult
    {
        public List<League> result { get; set; }
    }
}
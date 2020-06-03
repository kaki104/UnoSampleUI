using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UnoSampleUI.Models
{
    public class RSSChannel
    {
        public string title { get; set; }

        public string link { get; set; }

        public string description { get; set;}

        public DateTime pubdate { get; set; }

        public string language { get; set; }

        public string copyright { get; set; }

        public string webmaster { get; set; }

        public string generator { get; set; }

        public string docs { get; set; }

        public int ttl { get; set; }

        public RSSImage image { get; set; }

        public RSSItem currentitem { get; set; }

        public string author { get; set; }

        public string id { get; set; }

        public List<RSSItem> item;
    }

}

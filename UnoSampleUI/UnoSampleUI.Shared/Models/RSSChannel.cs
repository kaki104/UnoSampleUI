using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UnoSampleUI.Models
{
    public class RSSChannel
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set;}

        public DateTime Pubdate { get; set; }

        public string Language { get; set; }

        public string Copyright { get; set; }

        public string Webmaster { get; set; }

        public string Generator { get; set; }

        public string Docs { get; set; }

        public int Ttl { get; set; }

        public RSSImage Image { get; set; }

        public RSSItem Currentitem { get; set; }

        public string Author { get; set; }

        public string Id { get; set; }

        public List<RSSItem> Items;
    }

}

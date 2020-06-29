using System;

namespace UnoSampleUI.Models
{
    public class RSSItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ItemLink { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime Pubdate { get; set; }

        public DateTime Update { get; set; }

        public string ImageUrl { get; set; }

        public string ImageTitle { get; set; }

        public string ImageLink { get; set; }
    }
}

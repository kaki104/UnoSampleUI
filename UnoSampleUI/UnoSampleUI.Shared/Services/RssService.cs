using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnoSampleUI.Commons;
using UnoSampleUI.Models;
using UnoSampleUI.ViewModels;

namespace UnoSampleUI.Services
{
    public class RssService
    {
        private const string BAD_URL_MESSAGE = "Hmm... Are you sure this is an RSS URL?";
        private const string NO_REFRESH_MESSAGE = "Sorry. We can't get more articles right now.";

        /// <summary>
        /// Retrieves feed data from the server and updates the appropriate FeedViewModel properties.
        /// </summary>
        public static async Task<bool> TryGetFeedAsync(FeedViewModel feedViewModel, CancellationToken? cancellationToken = null)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    string result = await hc.GetStringAsync(feedViewModel.Link);
                    XDocument xdoc = XDocument.Parse(result);

                    RSSChannel feed = (from channel in xdoc.Descendants("channel")
                                       let hasImage = string.IsNullOrEmpty(channel.Element("image").GetString()) == true ? false : true
                                       select new RSSChannel()
                                       {
                                           Title = channel.Element("title").GetString(),
                                           Link = channel.Element("link").GetString(),
                                           Description = channel.Element("description").GetString(),
                                           Pubdate = channel.Element("pubDate").GetDateTime(),
                                           Language = channel.Element("language").GetString(),
                                           Copyright = channel.Element("copyright").GetString(),
                                           Webmaster = channel.Element("webMaster").GetString(),
                                           Generator = channel.Element("generator").GetString(),
                                           Docs = channel.Element("docs").GetString(),
                                           Ttl = channel.Element("ttl").GetInt(),
                                           Image = hasImage ? new RSSImage()
                                           {
                                               Url = channel.Element("image").Element("url").GetString(),
                                               Title = channel.Element("image").Element("title").GetString(),
                                               Link = channel.Element("image").Element("link").GetString(),
                                           } : null,
                                           Items = new List<RSSItem>()
                                       }).FirstOrDefault();

                    (from item in xdoc.Descendants("item")
                     let hasImage = string.IsNullOrEmpty(item.Element("image").GetString()) == true ? false : true
                     select new RSSItem()
                     {
                         Title = item.Element("title").GetString(),
                         Link = item.Element("link").GetString(),
                         Description = item.Element("description").GetString(),
                         Category = item.Element("category").GetString(),
                         Pubdate = item.Element("pubDate").GetDateTime(),
                         ImageLink = hasImage ? item.Element("image").Element("link").GetString() : item.Element("link").GetString(),
                         ImageTitle = hasImage ? item.Element("image").Element("title").GetString() : item.Element("title").GetString(),
                         ImageUrl = hasImage ? item.Element("image").Element("url").GetString() : null,
                     })
                        .ToList()
                        .ForEach(i => feed.Items.Add(i));

                    //var feed = await new SyndicationClient().RetrieveFeedAsync(feedViewModel.Link);

                    if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested)
                    {
                        return false;
                    }

                    feedViewModel.LastSyncDateTime = DateTime.Now;
                    feedViewModel.Name = string.IsNullOrEmpty(feedViewModel.Name) ? feed.Title : feedViewModel.Name;
                    feedViewModel.Description = feed.Description;
                    feedViewModel.Articles = feed.Items;
                    feedViewModel.IsInError = false;
                    feedViewModel.ErrorMessage = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (!cancellationToken.HasValue || !cancellationToken.Value.IsCancellationRequested)
                {
                    feedViewModel.IsInError = true;
                    feedViewModel.ErrorMessage = feedViewModel.Articles.Count == 0 ? BAD_URL_MESSAGE : NO_REFRESH_MESSAGE;
                }
                return false;
            }
        }

    }
}

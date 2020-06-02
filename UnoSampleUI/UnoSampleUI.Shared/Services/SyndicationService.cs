using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnoSampleUI.Shared.Models;
using UnoSampleUI.ViewModels;
using Windows.Web.Syndication;

namespace UnoSampleUI.Services
{
    public class SyndicationService
    {
        /// <summary>
        /// Retrieves feed data from the server and updates the appropriate FeedViewModel properties.
        /// </summary>
        public static async Task<bool> TryGetFeedAsync(FeedViewModel feedViewModel, CancellationToken? cancellationToken = null)
        {
            try
            {
                var feed = await new SyndicationClient().RetrieveFeedAsync(feedViewModel.Link);

                if (cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested) return false;

                feedViewModel.LastSyncDateTime = DateTime.Now;
                feedViewModel.Name = String.IsNullOrEmpty(feedViewModel.Name) ? feed.Title.Text : feedViewModel.Name;
                feedViewModel.Description = feed.Subtitle?.Text ?? feed.Title.Text;

                feed.Items.Select(item => new ArticleModel
                {
                    Title = item.Title.Text,
                    Summary = item.Summary == null ? string.Empty :
                        item.Summary.Text.RegexRemove("\\&.{0,4}\\;").RegexRemove("<.*?>"),
                    Author = item.Authors.Select(a => a.NodeValue).FirstOrDefault(),
                    Link = item.ItemUri ?? item.Links.Select(l => l.Uri).FirstOrDefault(),
                    PublishedDate = item.PublishedDate
                })
                .ToList().ForEach(article =>
                {
                    var favorites = AppShell.Current.ViewModel.FavoritesFeed;
                    var existingCopy = favorites.Articles.FirstOrDefault(a => a.Equals(article));
                    article = existingCopy ?? article;
                    if (!feedViewModel.Articles.Contains(article)) feedViewModel.Articles.Add(article);
                });
                feedViewModel.IsInError = false;
                feedViewModel.ErrorMessage = null;
                return true;
            }
            catch (Exception)
            {
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

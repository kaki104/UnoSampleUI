using GalaSoft.MvvmLight;
using System;

namespace UnoSampleUI.Shared.Models
{
    /// <summary>
    /// Represents an article in an RSS feed and user interactions with the article. 
    /// </summary>
    public class ArticleModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a summary describing the article contents. 
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets a string that indicates the article author(s). 
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the URI of the article. 
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the date that the article was published. 
        /// </summary>
        public DateTimeOffset PublishedDate { get; set; }

        /// <summary>
        /// Gets a formatted version of the article's publication date. 
        /// </summary>
        public string PublishedDateFormatted => PublishedDate.ToString("MMM dd, yyyy    h:mm tt").ToUpper();

        ///// <summary>
        ///// Updates the FavoritesFeed when an article is starred or unstarred. 
        ///// </summary>
        //public void SyncFavoritesFeed() => AppShell.Current.ViewModel.SyncFavoritesFeed(this);

        /// <summary>
        /// Determines whether the specified object is equal to the current object. 
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is ArticleModel ? (obj as ArticleModel).GetHashCode() == GetHashCode() : false;
        }

        /// <summary>
        /// Returns the hash code of the ArticleViewModel, which is based on 
        /// a string representation the Link value, using only the host and path.  
        /// </summary>
        public override int GetHashCode()
        {
            return Link.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped).GetHashCode();
        }

        private bool? _isStarred = false;
        /// <summary>
        /// Gets or sets a value that indicates whether the user has starred the article. 
        /// </summary>
        public bool? IsStarred
        {
            get => _isStarred;
            set => Set(ref _isStarred, value);
        }
    }
}

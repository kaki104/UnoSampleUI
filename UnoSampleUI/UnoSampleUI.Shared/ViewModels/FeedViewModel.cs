using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnoSampleUI.Models;
using UnoSampleUI.Services;
using UnoSampleUI.Shared.Models;
using Windows.UI.Xaml.Controls;

namespace UnoSampleUI.ViewModels
{
    public class FeedViewModel : ViewModelBase
    {
        public IList<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        /// <summary>
        /// Initializes a new instance of the FeedViewModel class. 
        /// </summary>
        public FeedViewModel()
        {
            Articles = new ObservableCollection<ArticleModel>();

            Init();
        }

        private async void Init()
        {
            ((INotifyCollectionChanged)Articles).CollectionChanged += FeedViewModel_CollectionChanged;
            //Link = new Uri("https://kaki104.tistory.com/rss");
            //await SyndicationService.TryGetFeedAsync(this);

            var datas = await SampleDataService.GetContentGridDataAsync();
            foreach (var item in datas)
            {
                Source.Add(item);
            }

        }

        private void FeedViewModel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsEmpty));
            RaisePropertyChanged(nameof(IsNotEmpty));
            RaisePropertyChanged(nameof(IsInErrorAndEmpty));
            RaisePropertyChanged(nameof(IsInErrorAndNotEmpty));
            RaisePropertyChanged(nameof(IsLoadingAndNotEmpty));
        }

        public override void Cleanup()
        {
            if (Articles == null)
            {
                return;
            } ((INotifyCollectionChanged)Articles).CollectionChanged -= FeedViewModel_CollectionChanged;
        }

        /// <summary>
        /// Gets or sets the URI of the feed.
        /// </summary>
        public Uri Link
        {
            get => _link;
            set
            {
                if (Set(ref _link, value))
                {
                    RaisePropertyChanged(nameof(LinkAsString));
                }
            }
        }
        private Uri _link;

        /// <summary>
        /// Gets or sets a string representation of the URI of the feed.
        /// </summary>
        //[IgnoreDataMember]
        public string LinkAsString
        {
            get => Link?.OriginalString ?? string.Empty;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                if (!value.Trim().StartsWith("http://") && !value.Trim().StartsWith("https://"))
                {
                    IsInError = true;
                    ErrorMessage = NOT_HTTP_MESSAGE;
                }
                else
                {
                    if (Uri.TryCreate(value.Trim(), UriKind.Absolute, out Uri uri))
                    {
                        Link = uri;
                    }
                    else
                    {
                        IsInError = true;
                        ErrorMessage = INVALID_URL_MESSAGE;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the name of the feed.
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        private string _name;

        /// <summary>
        /// Gets a description of the feed.
        /// </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }
        private string _description;

        /// <summary>
        /// Gets the symbol that represents the feed in the navigation pane.
        /// </summary>
        public Symbol Symbol
        {
            get => _symbol;
            set
            {
                if (Set(ref _symbol, value))
                {
                    RaisePropertyChanged(nameof(SymbolAsChar));
                }
            }
        }
        private Symbol _symbol;

        /// <summary>
        /// Gets a character representation of the symbol that represents the feed in the navigation pane.
        /// </summary>
        public char SymbolAsChar => (char)Symbol;

        /// <summary>
        /// Gets the collection of articles that have been loaded for this feed. 
        /// </summary>
        public IList<ArticleModel> Articles { get; }

        /// <summary>
        /// Gets the articles collection as an instance of type Object. 
        /// </summary>
        public object ArticlesAsObject => Articles;

        /// <summary>
        /// Gets a value that indicates whether the articles collection is empty. 
        /// </summary>
        public bool IsEmpty => Articles.Count == 0;

        /// <summary>
        /// Gets a value that indicates whether the feed has at least one article.
        /// </summary>
        public bool IsNotEmpty => !IsEmpty;

        /// <summary>
        /// Gets or sets a value that indicates whether the feed represents the collection of starred articles.
        /// </summary>
        public bool IsFavoritesFeed { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the feed is a normal (non-favorites) feed that isn't in error.
        /// </summary>
        public bool IsNotFavoritesOrInError => !IsFavoritesFeed && !IsInError;

        /// <summary>
        /// Gets or sets the date and time of the last successful article retrieval. 
        /// </summary>
        public DateTime LastSyncDateTime
        {
            get => _lastSyncDateTime;
            set => Set(ref _lastSyncDateTime, value);
        }
        private DateTime _lastSyncDateTime;

        /// <summary>
        /// Gets a message to display when new articles cannot be retrieved. 
        /// </summary>
        public string FeedDownMessage
        {
            get
            {
                string lastSync = LastSyncDateTime.ToString(LastSyncDateTime.Date == DateTime.Today ? "t" : "g");
                return $"It looks like this feed is down. Last synced {lastSync}. Tap here to refresh.";
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the feed is the current selection in the navigation pane. 
        /// </summary>
        //[IgnoreDataMember]
        public bool IsSelectedInNavList
        {
            get => _isSelectedInNavList;
            set => Set(ref _isSelectedInNavList, value);
        }
        //[IgnoreDataMember] 
        private bool _isSelectedInNavList;

        /// <summary>
        /// Gets or sets a value that indicates whether the feed is currently loading article data. 
        /// </summary>
        //[IgnoreDataMember]
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (Set(ref _isLoading, value))
                {
                    RaisePropertyChanged(nameof(IsInError));
                    RaisePropertyChanged(nameof(IsLoadingAndNotEmpty));
                    RaisePropertyChanged(nameof(IsNotFavoritesOrInError));
                    RaisePropertyChanged(nameof(IsInErrorAndEmpty));
                    RaisePropertyChanged(nameof(IsInErrorAndNotEmpty));
                }
            }
        }
        //[IgnoreDataMember]
        private bool _isLoading;

        public bool IsLoadingAndNotEmpty => IsLoading && !IsEmpty;

        /// <summary>
        /// Gets or sets a value that indicates whether the feed is currently being renamed. 
        /// </summary>
        //[IgnoreDataMember]
        public bool IsInEdit
        {
            get => _isInEdit;
            set => Set(ref _isInEdit, value);
        }
        //[IgnoreDataMember]
        private bool _isInEdit;

        /// <summary>
        /// Gets or sets a value that indicates whether the feed is currently in an error state
        /// and is no longer trying to retrieve new data. 
        /// </summary>
        //[IgnoreDataMember]
        public bool IsInError
        {
            get => _isInError && !IsLoading;
            set
            {
                if (Set(ref _isInError, value))
                {
                    RaisePropertyChanged(nameof(IsNotFavoritesOrInError));
                    RaisePropertyChanged(nameof(IsInErrorAndEmpty));
                    RaisePropertyChanged(nameof(IsInErrorAndNotEmpty));
                }
            }
        }
        //[IgnoreDataMember]
        private bool _isInError;

        /// <summary>
        /// Gets a value that indicates whether the feed is both in error and has no articles. 
        /// </summary>
        public bool IsInErrorAndEmpty => IsInError && IsEmpty;

        /// <summary>
        ///  Gets a value that indicates whether the feed is in error, but has already retrieved some articles. 
        /// </summary>
        public bool IsInErrorAndNotEmpty => IsInError && !IsEmpty;

        /// <summary>
        /// Gets or sets the description of the current error, if the feed is in an error state. 
        /// </summary>
        //[IgnoreDataMember]
        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }
        //[IgnoreDataMember]
        public string _errorMessage;

        /// <summary>
        /// Determines whether the specified object is equal to the current object. 
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is FeedViewModel ? (obj as FeedViewModel).GetHashCode() == GetHashCode() : false;
        }

        /// <summary>
        /// Returns the hash code of the FeedViewModel, which is based on 
        /// a string representation the Link value, using only the host and path.  
        /// </summary>
        public override int GetHashCode()
        {
            return Link?.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped).GetHashCode() ?? 0;
        }

        private const string NOT_HTTP_MESSAGE = "Sorry. The URL must begin with http:// or https://";
        private const string INVALID_URL_MESSAGE = "Sorry. That is not a valid URL.";
    }
}

using UnoSampleUI.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoSampleUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeedPage : Page
    {
        public FeedPage()
        {
            InitializeComponent();
            ViewModel = ViewModelLocator.Current.Feed;
        }

        public FeedViewModel ViewModel
        {
            get => DataContext as FeedViewModel;
            set => DataContext = value;
        }
    }
}

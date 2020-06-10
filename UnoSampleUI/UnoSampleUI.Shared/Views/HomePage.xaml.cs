using UnoSampleUI.ViewModels;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoSampleUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            if(!DesignMode.DesignMode2Enabled)
            {
                ViewModel = ViewModelLocator.Current.Home;
            }
        }

        /// <summary>
        /// ViewModel
        /// </summary>
        public HomeViewModel ViewModel
        {
            get
            {
                return DataContext as HomeViewModel;
            }
            set { DataContext = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //
        }
    }
}

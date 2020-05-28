using UnoSampleUI.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UnoSampleUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

        private MainViewModel ViewModel => ViewModelLocator.Current.Main;
    }
}

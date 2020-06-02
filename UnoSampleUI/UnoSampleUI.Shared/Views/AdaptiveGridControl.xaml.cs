using UnoSampleUI.ViewModels;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UnoSampleUI.Views
{
    public sealed partial class AdaptiveGridControl : UserControl
    {
        public AdaptiveGridControl()
        {
            InitializeComponent();
        }
        //ViewModelLocator.Current.AdaptiveGrid;
        public AdaptiveGridViewModel ViewModel 
        {
            get 
            {
                if(DesignMode.DesignMode2Enabled)
                {
                    return new AdaptiveGridViewModel();
                }
                else
                {
                    return ViewModelLocator.Current.AdaptiveGrid;
                }
            } 
        } 
    }
}

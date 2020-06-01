using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UnoSampleUI.Controls
{
    public sealed partial class PageHeaderControl : UserControl
    {
        //private static readonly double DEFAULT_LEFT_MARGIN = 24;

        public PageHeaderControl()
        {
            this.InitializeComponent();

            if (DesignMode.DesignMode2Enabled)
            {
                return;
            }
        }

        public UIElement HeaderContent
        {
            get => (UIElement)GetValue(HeaderContentProperty);
            set => SetValue(HeaderContentProperty, value);
        }

        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(UIElement), typeof(PageHeaderControl), 
                new PropertyMetadata(DependencyProperty.UnsetValue));

    }
}

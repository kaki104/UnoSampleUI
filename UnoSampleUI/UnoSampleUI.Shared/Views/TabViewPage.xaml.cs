﻿using UnoSampleUI.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoSampleUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabViewPage : Page
    {
        public TabViewPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ViewModel
        /// </summary>
        public TabViewViewModel ViewModel => ViewModelLocator.Current.TabView;
    }
}

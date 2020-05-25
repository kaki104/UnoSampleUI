using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace UnoSampleUI.Shared.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region IsBackEnabled

        private bool _isBackEnabled;
        public bool IsBackEnabled
        {
            get { return _isBackEnabled; }
            set { Set(ref _isBackEnabled, value); }
        }

        #endregion

        #region Selected

        private NavigationViewItem _selected;
        public NavigationViewItem Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        #endregion

        public ICommand ItemInvokedCommand { get; set; }

        public MainViewModel()
        {
            Init();
        }

        private void Init()
        {
            ItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked);
        }

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs obj)
        {
            
        }
    }
}

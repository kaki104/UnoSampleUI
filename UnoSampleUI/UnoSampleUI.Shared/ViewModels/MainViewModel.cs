using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoSampleUI.Helpers;
using UnoSampleUI.Services;
using UnoSampleUI.Shared.Commons;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace UnoSampleUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// static 네비게이션 서비스
        /// </summary>
        public static NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

#if NETFX_CORE
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator 
            = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        private readonly KeyboardAccelerator _backKeyboardAccelerator 
            = BuildKeyboardAccelerator(VirtualKey.GoBack);
#endif
        
        /// <summary>
        /// 네비게이션 뷰
        /// </summary>
        private NavigationView _navigationView;
        /// <summary>
        /// 키보드 엑셀레이터
        /// </summary>
        private IList<KeyboardAccelerator> _keyboardAccelerators;

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

        /// <summary>
        /// 아이템 임보크 커맨드
        /// </summary>
        public ICommand ItemInvokedCommand { get; set; }
        /// <summary>
        /// 로디드 커맨드
        /// </summary>
        public ICommand LoadedCommand { get; set; }

        /// <summary>
        /// 기본생성자
        /// </summary>
        public MainViewModel()
        {
            Init();
        }

#if NETFX_CORE
        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }
#endif

#if NETFX_CORE
        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var result = NavigationService.GoBack();
            args.Handled = result;
        }
#endif

        public void Initialize(Frame frame, NavigationView navigationView, IList<KeyboardAccelerator> keyboardAccelerators)
        {
            _navigationView = navigationView;
            _keyboardAccelerators = keyboardAccelerators;
            NavigationService.Frame = frame;
            NavigationService.NavigationFailed += Frame_NavigationFailed;
            NavigationService.Navigated += Frame_Navigated;
            _navigationView.BackRequested += OnBackRequested;

            //home
            NavigationService.Navigate(PageKeys.Home);
        }

        private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            NavigationService.GoBack();
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;
            Selected = _navigationView.MenuItems
                            .OfType<NavigationViewItem>()
                            .FirstOrDefault(menuItem => IsMenuItemForPageType(menuItem, e.SourcePageType));
        }

        private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
        {
            var navigatedPageKey = NavigationService.GetNameOfRegisteredPage(sourcePageType);
            var pageKey = menuItem.GetValue(NavHelper.NavigateToProperty) as string;
            return pageKey == navigatedPageKey;
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw e.Exception;
        }

        private void Init()
        {
            ItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked);
            LoadedCommand = new RelayCommand(OnLoaded);
        }

        private async void OnLoaded()
        {
#if NETFX_CORE
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            _keyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            _keyboardAccelerators.Add(_backKeyboardAccelerator);
#endif
            await Task.CompletedTask;
        }

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            var item = _navigationView.MenuItems
                .OfType<NavigationViewItem>()
                .First(menuItem => (string)menuItem.Content == (string)args.InvokedItem);
            var pageKey = item.GetValue(NavHelper.NavigateToProperty) as string;
            NavigationService.Navigate(pageKey);
        }
    }
}

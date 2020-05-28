using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using UnoSampleUI.Services;
using UnoSampleUI.Shared.ControlViewModels;
using UnoSampleUI.Views;
using Windows.UI.Xaml.Navigation;

namespace UnoSampleUI.ViewModels
{
    public class ViewModelLocator
    {
        #region Current

        private static ViewModelLocator _current;
        /// <summary>
        /// Current
        /// </summary>
        public static ViewModelLocator Current
        {
            get
            {
                if(_current == null)
                {
                    _current = new ViewModelLocator();
                }
                return _current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public static void SetViewModelLocator(ViewModelLocator source)
        {
            _current = source;
        }

        #endregion
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NavigationServiceEx>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<ContentGridViewModel>();
            SimpleIoc.Default.Register<TabViewViewModel>();
            SimpleIoc.Default.Register<AdaptiveGridViewModel>();

            //뷰모델과 뷰연결 - 네비게이션을 하는 경우에만 사용
            Register<HomeViewModel, HomePage>();
            Register<ContentGridViewModel, ContentGridPage>();
            Register<TabViewViewModel, TabViewPage>();

        }

        /// <summary>
        /// MainViewModel
        /// </summary>
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// HomeViewModel
        /// </summary>
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();

        /// <summary>
        /// ContnetGrid
        /// </summary>
        public ContentGridViewModel ContentGrid => ServiceLocator.Current.GetInstance<ContentGridViewModel>();

        /// <summary>
        /// TabView
        /// </summary>
        public TabViewViewModel TabView => ServiceLocator.Current.GetInstance<TabViewViewModel>();

        /// <summary>
        /// AdaptiveGrid 
        /// </summary>
        public AdaptiveGridViewModel AdaptiveGrid => ServiceLocator.Current.GetInstance<AdaptiveGridViewModel>();

        /// <summary>
        /// 네비게이션 서비스 - 뷰모델 연결하는게 좀 마음에 들지 않음
        /// </summary>
        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();

        /// <summary>
        /// 클린업
        /// </summary>
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        /// <summary>
        /// 뷰모델과 뷰연결
        /// </summary>
        /// <typeparam name="VM"></typeparam>
        /// <typeparam name="V"></typeparam>
        public void Register<VM, V>() where VM : class
        {
            SimpleIoc.Default.Register<VM>();
            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}

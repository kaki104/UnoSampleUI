using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using UnoSampleUI.Services;
using UnoSampleUI.Shared.ControlViewModels;
using UnoSampleUI.Views;
using Windows.ApplicationModel;

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
                if (_current == null)
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
            if (DesignMode.DesignMode2Enabled) return;

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
            SimpleIoc.Default.Register<NavigationServiceEx>();
            SimpleIoc.Default.Register<RssService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<ContentGridViewModel>();
            SimpleIoc.Default.Register<TabViewViewModel>();
            SimpleIoc.Default.Register<AdaptiveGridViewModel>();
            SimpleIoc.Default.Register<FeedViewModel>();


            //뷰모델과 뷰연결 - 네비게이션을 하는 경우에만 사용
            Register<HomeViewModel, HomePage>();
            Register<ContentGridViewModel, ContentGridPage>();
            Register<TabViewViewModel, TabViewPage>();
            Register<FeedViewModel, FeedPage>();

        }

        /// <summary>
        /// MainViewModel
        /// </summary>
        public MainViewModel Main => GetInstance<MainViewModel>();

        /// <summary>
        /// HomeViewModel
        /// </summary>
        public HomeViewModel Home => GetInstance<HomeViewModel>();

        /// <summary>
        /// ContnetGrid
        /// </summary>
        public ContentGridViewModel ContentGrid => GetInstance<ContentGridViewModel>();

        /// <summary>
        /// TabView
        /// </summary>
        public TabViewViewModel TabView => GetInstance<TabViewViewModel>();

        /// <summary>
        /// AdaptiveGrid 
        /// </summary>
        public AdaptiveGridViewModel AdaptiveGrid => GetInstance<AdaptiveGridViewModel>();

        /// <summary>
        /// Feed
        /// </summary>
        public FeedViewModel Feed => GetInstance<FeedViewModel>();

        /// <summary>
        /// 네비게이션 서비스 - 뷰모델 연결하는게 좀 마음에 들지 않음
        /// </summary>
        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();

        /// <summary>
        /// 신디케이션 서비스
        /// </summary>
        public RssService SyndicationService => SimpleIoc.Default.GetInstance<RssService>();

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetInstance<T>() where T : class
        {
            if (DesignMode.DesignMode2Enabled)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                return (T)ServiceLocator.Current.GetInstance(typeof(T));
            }
        }

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

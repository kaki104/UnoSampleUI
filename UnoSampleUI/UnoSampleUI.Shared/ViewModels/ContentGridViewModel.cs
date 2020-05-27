using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoSampleUI.Models;
using UnoSampleUI.Services;

namespace UnoSampleUI.ViewModels
{
    public class ContentGridViewModel : ViewModelBase
    {
        /// <summary>
        /// 네비게이션 서비스 - 굿이 이렇게 않해도 될 것 같은데..
        /// </summary>
        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public IList<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public ICommand ItemClickCommand { get; set; }

        public ContentGridViewModel()
        {
            Init();
        }

        private async void Init()
        {
            ItemClickCommand = new RelayCommand<SampleOrder>(OnItemClick);

            var datas = await SampleDataService.GetContentGridDataAsync();
            foreach (var item in datas)
            {
                Source.Add(item);
            }
        }

        private void OnItemClick(SampleOrder obj)
        {
            
        }
    }
}

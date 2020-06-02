using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnoSampleUI.Models;
using UnoSampleUI.Services;

namespace UnoSampleUI.ViewModels
{
    public class FeedViewModel : ViewModelBase
    {
        public IList<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public FeedViewModel()
        {
            Init();
        }

        private async void Init()
        {
            //ItemClickCommand = new RelayCommand<SampleOrder>(OnItemClick);

            var datas = await SampleDataService.GetContentGridDataAsync();
            foreach (var item in datas)
            {
                Source.Add(item);
            }
        }

    }
}

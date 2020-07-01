using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using UnoSampleUI.Models;
using UnoSampleUI.Services;

namespace UnoSampleUI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public IList<SampleOrder> Items { get; } = new ObservableCollection<SampleOrder>();

        public ICommand ItemClickCommand { get; set; }

        public ICommand BackCommand { get; set; }

        public HomeViewModel()
        {
            Init();
        }

        private async void Init()
        {
            ItemClickCommand = new RelayCommand<SampleOrder>(OnItemClick);
            BackCommand = new RelayCommand(OnBack);

            var datas = await SampleDataService.GetContentGridDataAsync();
            foreach (var item in datas)
            {
                Items.Add(item);
            }
        }

        private void OnBack()
        {
            CurrentItem = null;
        }

        private void OnItemClick(SampleOrder obj)
        {
            throw new NotImplementedException();
        }

        private SampleOrder currentItem;
        /// <summary>
        /// CurrentItem
        /// </summary>
        public SampleOrder CurrentItem
        {
            get { return currentItem; }
            set { Set(ref currentItem ,value); }
        }

    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using UnoSampleUI.Models;
using UnoSampleUI.Services;
using System.Collections.ObjectModel;

namespace UnoSampleUI.ViewModels
{
    /// <summary>
    /// 아답티브 그리드 뷰모델
    /// </summary>
    public class AdaptiveGridViewModel : ViewModelBase
    {
        /// <summary>
        /// 아이템 클릭 커맨드
        /// </summary>
        public ICommand ItemClickCommand { get; set; }

        private IList<SampleOrder> _source;
        /// <summary>
        /// Source
        /// </summary>
        public IList<SampleOrder> Source
        {
            get { return _source; }
            set { Set(ref _source ,value); }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public AdaptiveGridViewModel()
        {
            Init();
        }

        private async void Init()
        {
            ItemClickCommand = new RelayCommand<SampleOrder>(OnItemClick);

            if(IsInDesignMode)
            {
                Source = new List<SampleOrder>(await SampleDataService.GetContentGridDataAsync());
            }
            else
            {
                Source = new ObservableCollection<SampleOrder>();
                var datas = await SampleDataService.GetContentGridDataAsync();
                foreach (var item in datas)
                {
                    Source.Add(item);
                }
            }
        }

        private void OnItemClick(SampleOrder obj)
        {
            
        }
    }
}

using ListToItemsSource.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace ListToItemsSource.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        private IList<Code> _codes;
        /// <summary>
        /// Codes
        /// </summary>
        public IList<Code> Codes
        {
            get { return _codes; }
            set { SetProperty(ref _codes, value); }
        }

        public MainViewModel()
        {
            Codes = new List<Code>
            {
                new Code{ CodeId = "CodeId1", Name = "Name1", Value = "Value1"},
                new Code{ CodeId = "CodeId2", Name = "Name2", Value = "Value2"},
                new Code{ CodeId = "CodeId3", Name = "Name3", Value = "Value3"},
                new Code{ CodeId = "CodeId4", Name = "Name4", Value = "Value4"},
                new Code{ CodeId = "CodeId5", Name = "Name5", Value = "Value5"},
            };
        }
    }
}

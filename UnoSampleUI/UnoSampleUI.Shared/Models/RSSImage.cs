using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnoSampleUI.Models
{
    public class RSSImage : ObservableObject
    {
        private string _url;
        public string url 
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }

        public string title { get; set; }

        public string link { get; set; }
    }
}

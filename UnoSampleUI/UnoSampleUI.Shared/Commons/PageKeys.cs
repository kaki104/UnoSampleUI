using System;
using System.Collections.Generic;
using System.Text;
using UnoSampleUI.ViewModels;

namespace UnoSampleUI.Shared.Commons
{

    public static class PageKeys
    {
        public static string Home { get; } = typeof(HomeViewModel).FullName;
    }
}

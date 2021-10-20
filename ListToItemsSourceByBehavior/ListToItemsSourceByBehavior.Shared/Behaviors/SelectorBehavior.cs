using ListToItemsSourceByBehavior.Models;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ListToItemsSourceByBehavior.Behaviors
{
    public class SelectorBehavior : Behavior<Selector>
    {
        private IList<Code> _codes = new List<Code>
            {
                new Code{ CodeId = "CodeId1", Name = "Name1", Value = "Value1"},
                new Code{ CodeId = "CodeId2", Name = "Name2", Value = "Value2"},
                new Code { CodeId = "CodeId3", Name = "Name3", Value = "Value3" },
                new Code { CodeId = "CodeId4", Name = "Name4", Value = "Value4" },
                new Code { CodeId = "CodeId5", Name = "Name5", Value = "Value5" },
            };

        public SelectorBehavior()
        {
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AssociatedObject.ItemsSource = _codes;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
    }
}

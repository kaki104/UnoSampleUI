using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UnoSampleUI.Controls
{
    public class NavListView : ListView, IDisposable
    {
        public NavListView()
        {
            SelectionMode = ListViewSelectionMode.Single;
            IsItemClickEnabled = true;
            ItemClick += ItemClickHandler;
            Loaded += LoadedHandler;
        }

        /// <summary>
        /// Occurs when an item has been selected
        /// </summary>
        public event EventHandler<ListViewItem> ItemInvoked;

        private void LoadedHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void ItemClickHandler(object sender, ItemClickEventArgs e)
        {

        }

        public void SetSelectedItem(ListViewItem item)
        {
            SelectedItem = item;
        }

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            object focusedItem = FocusManager.GetFocusedElement();
            switch (e.Key)
            {
                case VirtualKey.Up:
                    //방향키업
                    TryMoveFocus(FocusNavigationDirection.Up);
                    e.Handled = true;
                    break;

                case VirtualKey.Down:
                    //방향키다운
                    TryMoveFocus(FocusNavigationDirection.Down);
                    e.Handled = true;
                    break;

                case VirtualKey.Tab:
                    OnTabKeyDown(focusedItem);
                    e.Handled = true;
                    break;

                case VirtualKey.Space:
                case VirtualKey.Enter:
                    // Fire our event using the item with current keyboard focus
                    InvokeItem(focusedItem);
                    e.Handled = true;
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void InvokeItem(object focusedItem)
        {
            this.SetSelectedItem(focusedItem as ListViewItem);
            this.ItemInvoked?.Invoke(this, focusedItem as ListViewItem);

            //if (this.splitViewHost == null || this.splitViewHost.IsPaneOpen)
            //{
            //    if (this.splitViewHost != null &&
            //        (this.splitViewHost.DisplayMode == SplitViewDisplayMode.CompactOverlay ||
            //        this.splitViewHost.DisplayMode == SplitViewDisplayMode.Overlay))
            //    {
            //        this.splitViewHost.IsPaneOpen = false;
            //    }
            //    if (focusedItem is ListViewItem)
            //    {
            //        ((ListViewItem)focusedItem).Focus(FocusState.Programmatic);
            //    }
            //}
        }


        /// <summary>
        /// 탭키다운
        /// </summary>
        /// <param name="focusedItem"></param>
        private void OnTabKeyDown(object focusedItem)
        {
            CoreVirtualKeyStates shiftKeyState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Shift);
            bool shiftKeyDown = (shiftKeyState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;

            // If we're on the header item then this will be null and we'll still get the default behavior.
            switch (focusedItem)
            {
                case ListViewItem focusedListViewItem:
                    bool onLastItem = IndexFromContainer(focusedListViewItem) == Items.Count - 1;
                    bool onFirstItem = IndexFromContainer(focusedListViewItem) == 0;
                    if (!shiftKeyDown)
                    {
                        //tab키를 눌렀을 때 쉬프트키를 누른 상태가 아니라면 기본적으로는 Down이고 마지막 아이템이면 Next
                        if (onLastItem)
                        {
                            TryMoveFocus(FocusNavigationDirection.Next);
                        }
                        else
                        {
                            TryMoveFocus(FocusNavigationDirection.Down);
                        }
                    }
                    else
                    {
                        //Shift + Tab
                        if (onFirstItem)
                        {
                            TryMoveFocus(FocusNavigationDirection.Previous);
                        }
                        else
                        {
                            TryMoveFocus(FocusNavigationDirection.Up);
                        }
                    }
                    break;

                case Control focusedControl:
                    if (!shiftKeyDown)
                    {
                        TryMoveFocus(FocusNavigationDirection.Down);
                    }
                    else
                    {
                        TryMoveFocus(FocusNavigationDirection.Up);
                    }
                    break;
            }
        }

        /// <summary>
        /// This method is a work-around until the bug in FocusManager.TryMoveFocus is fixed.
        /// </summary>
        /// <param name="direction"></param>
        private void TryMoveFocus(FocusNavigationDirection direction)
        {
            if (direction == FocusNavigationDirection.Next || direction == FocusNavigationDirection.Previous)
            {
                FocusManager.TryMoveFocus(direction);
            }
            else
            {
                Control control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null)
                {
                    control.Focus(Windows.UI.Xaml.FocusState.Programmatic);
                }
                if (control is ListViewItem)
                {
                    ListViewItem item = control as ListViewItem;
                    ScrollIntoView(item.Content);
                }
            }
        }

        public void Dispose()
        {
            
            ItemClick -= ItemClickHandler;
            Loaded -= LoadedHandler;

        }
    }
}

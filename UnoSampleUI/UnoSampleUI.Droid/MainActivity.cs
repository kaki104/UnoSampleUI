using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Views;

namespace UnoSampleUI.Droid
{
	[Activity(
			MainLauncher = true,
			ConfigurationChanges = Uno.UI.ActivityHelper.AllConfigChanges,
			WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden
		)]
	public class MainActivity : Windows.UI.Xaml.ApplicationActivity
	{
        public override void OnBackPressed()
        {
			GalaSoft.MvvmLight.Messaging.Messenger.Default.Send("OnBackPressed");
		}
    }
}


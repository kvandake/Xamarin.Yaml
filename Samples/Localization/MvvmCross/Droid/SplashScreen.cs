namespace Yaml.Localization.MvvmCross.Droid
{
	using Android.App;
	using Android.Content.PM;
	using global::MvvmCross.Droid.Views;

	[Activity(
		Label = "Sample.MvvmCross"
		, MainLauncher = true
		, Icon = "@mipmap/ic_launcher"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}
	}
}

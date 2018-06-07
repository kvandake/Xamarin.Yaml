namespace Yaml.Localization.MvvmCross.iOS
{
	using Foundation;
	using global::MvvmCross.Core.ViewModels;
	using global::MvvmCross.iOS.Platform;
	using global::MvvmCross.Platform;
	using UIKit;

	[Register("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

			var setup = new Setup(this, this.Window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			this.Window.MakeKeyAndVisible();

			return true;
		}
	}
}

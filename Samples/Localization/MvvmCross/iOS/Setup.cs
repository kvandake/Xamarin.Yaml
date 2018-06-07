namespace Yaml.Localization.MvvmCross.iOS
{
	using System.Globalization;
	using global::MvvmCross.Core.ViewModels;
	using global::MvvmCross.iOS.Platform;
	using global::MvvmCross.iOS.Views.Presenters;
	using global::MvvmCross.Localization;
	using global::MvvmCross.Platform.Converters;
	using global::MvvmCross.Platform.Platform;
	using UIKit;
	using Yaml.Localization.MvvmCross;

	public class Setup : MvxIosSetup
	{
		private App app = new App();
		
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
			: base(applicationDelegate, window)
		{
		}

		public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return this.app;
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}
		
		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
			base.FillValueConverters(registry);
			registry.AddOrOverwrite("Language", new MvxLanguageConverter());
		}

		public override void InitializeSecondary()
		{
			base.InitializeSecondary();
			this.app.InitializeCultureInfo(new CultureInfo("en-US"));
		}
	}
}

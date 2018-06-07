namespace Yaml.Localization.MvvmCross
{
	using System.Globalization;
	using System.Reflection;
	using Acr.UserDialogs;
	using global::MvvmCross.Core.ViewModels;
	using global::MvvmCross.Localization;
	using global::MvvmCross.Platform;
	using global::MvvmCross.Platform.IoC;
	using global::MvvmCross.Platform.Logging;
	using Xamarin.Yaml.Localization.Configs;
	using Xamarin.Yaml.Parser;
	using Yaml.Localization.MvvmCross.ViewModels;
	using YamlLocalization;

	public class App : MvxApplication
	{
		private static IMvxLog localeLog;

		private static IMvxLog LocaleLog => localeLog ?? (localeLog = CreateMvxLog());

		private static IMvxLog CreateMvxLog()
		{
			return Mvx.Resolve<IMvxLogProvider>().GetLogFor("MvxLocale");
		}
		
		public override void Initialize()
		{
			this.CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			this.RegisterNavigationServiceAppStart<FirstViewModel>();

			Mvx.RegisterSingleton(UserDialogs.Instance);
			var assemblyConfig = new AssemblyContentConfig(this.GetType().GetTypeInfo().Assembly)
			{
				ResourceFolder = "Locales",
				ParserConfig = new ParserConfig
				{
					ThrowWhenKeyNotFound = true
				},
				Logger = trace => { LocaleLog.Debug(trace); }
			};

			var textProvider = new MvxYamlTextProvider(assemblyConfig);

			Mvx.RegisterSingleton<IMvxTextProvider>(textProvider);
			Mvx.RegisterSingleton<IMvxLocalizationProvider>(textProvider);
		}

		public void InitializeCultureInfo(CultureInfo cultureInfo)
		{
			var localizationProvider = Mvx.Resolve<IMvxLocalizationProvider>();
			localizationProvider.ChangeLocale(cultureInfo).Wait();
		}
	}
}
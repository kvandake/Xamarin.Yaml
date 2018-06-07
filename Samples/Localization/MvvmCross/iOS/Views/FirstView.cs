namespace Yaml.Localization.MvvmCross.iOS.Views
{
	using System;
	using global::MvvmCross.Binding.BindingContext;
	using global::MvvmCross.iOS.Views;
	using Yaml.Localization.MvvmCross.Extensions;
	using Yaml.Localization.MvvmCross.ViewModels;

	[MvxFromStoryboard]
	public partial class FirstView : MvxViewController
	{
		public FirstView(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var set = this.CreateBindingSet<FirstView, FirstViewModel>();
			set.Bind(this).For(v => v.Title).ToFlyLocalizationId("Title");
			set.Bind(this.Label).ToFlyLocalizationId("Label");
			set.Bind(this.ChangeLocale).For("Title").ToFlyLocalizationId("Buttons.ChangeLocale");
			set.Bind(this.ChangeLocale).To(vm => vm.ChangeLocaleCommand);
			set.Apply();
		}
	}
}
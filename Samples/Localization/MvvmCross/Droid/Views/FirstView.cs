namespace Yaml.Localization.MvvmCross.Droid.Views
{
	using Android.App;
	using Android.OS;

	[Activity(Label = "View for FirstViewModel")]
	public class FirstView : BaseView
	{
		protected override int LayoutResource => Resource.Layout.FirstView;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			this.SupportActionBar.SetDisplayHomeAsUpEnabled(false);
		}
	}
}

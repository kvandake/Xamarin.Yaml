namespace Yaml.Localization.MvvmCross.Droid.Views
{
	using Android.OS;
	using Android.Support.V7.Widget;
	using global::MvvmCross.Droid.Support.V7.AppCompat;

	public abstract class BaseView : MvxAppCompatActivity
	{
		protected Toolbar Toolbar { get; set; }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			this.SetContentView(this.LayoutResource);

			this.Toolbar = this.FindViewById<Toolbar>(global::Yaml.Localization.MvvmCross.Droid.Resource.Id.toolbar);
			if (this.Toolbar != null)
			{
				this.SetSupportActionBar(this.Toolbar);
				this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
				this.SupportActionBar.SetHomeButtonEnabled(true);
			}
		}

		protected abstract int LayoutResource { get; }
	}
}

using Xamarin.Forms;
using Yaml.Localization.Sample.Droid.Renderers;
using Yaml.Localization.Sample.Views;

[assembly: ExportRenderer(typeof(LocaleSwitch), typeof(LocaleSwitchRenderer))]

namespace Yaml.Localization.Sample.Droid.Renderers
{
    using Android.Content;
    using Android.Graphics;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class LocaleSwitchRenderer : SwitchRenderer
    {
        public LocaleSwitchRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            // do whatever you want to the UISwitch here!
            this.Control?.ThumbDrawable.SetColorFilter(Xamarin.Forms.Color.FromHex("#E60000").ToAndroid(), PorterDuff.Mode.Multiply);
        }
    }
}
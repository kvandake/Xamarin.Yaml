using Xamarin.Forms;
using Yaml.Localization.Sample.iOS.Renderers;
using Yaml.Localization.Sample.Views;

[assembly: ExportRenderer(typeof(TranslateListView), typeof(TranslateListViewRenderer))]

namespace Yaml.Localization.Sample.iOS.Renderers
{
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class TranslateListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Control == null)
            {
                return;
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
            {
                this.Control.CellLayoutMarginsFollowReadableWidth = false;
            }
        }
    }
}
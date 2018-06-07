// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

namespace Yaml.Localization.MvvmCross.iOS.Views
{
    using System.CodeDom.Compiler;
    using Foundation;

    [Register ("FirstView")]
    partial class FirstView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ChangeLocale { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Label { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (this.ChangeLocale != null) {
                this.ChangeLocale.Dispose ();
                this.ChangeLocale = null;
            }

            if (this.Label != null) {
                this.Label.Dispose ();
                this.Label = null;
            }
        }
    }
}
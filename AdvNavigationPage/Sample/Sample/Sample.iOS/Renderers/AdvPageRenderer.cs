using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Sample.Controls;
using Sample.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(AdvPageRenderer))]
namespace Sample.iOS.Renderers
{
    public class AdvPageRenderer: PageRenderer
    {
        public ContentPage MyElement => Element as ContentPage;
        private UIView _statusBar;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //if (NavigationController != null)
            //{
            //    NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            //    NavigationController.NavigationBar.ShadowImage = new UIImage();
            //    NavigationController.NavigationBar.Translucent = true;
            //    //NavigationController.View.BackgroundColor = UIColor.Red;
            //    var page = Element as Page;
            //    UpdateToolbarBackground(AdvNavigationPage.GetBarBackgroundOpacity(page));

            //    //UpdateToolbarContentPosition(AdvNavigationPage.GetIsBarOverlap(page));

            //}
            //EdgesForExtendedLayout = UIRectEdge.None;

            //_statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Element.PropertyChanged += Element_PropertyChanged;
        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (e.PropertyName == AdvNavigationPage.BarBackgroundOpacityProperty.PropertyName)
            {
                UpdateToolbarBackground(AdvNavigationPage.GetBarBackgroundOpacity(page));
            }
        }

        private void UpdateToolbarBackground(double opacity)
        {
            if(NavigationController != null)
                NavigationController.NavigationBar.BackgroundColor = Color.Red.ToUIColor().
                    ColorWithAlpha((float)opacity);

            if(_statusBar != null)
                if (_statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    _statusBar.BackgroundColor = Color.Red.ToUIColor().
                        ColorWithAlpha((float)opacity);
                }
        }

        private void UpdateToolbarContentPosition(bool isOverlap)
        {
            NavigationController.NavigationBar.Translucent = isOverlap;
        }

        private void UpdateToolbarTitleAlignment(TextAlignment getTitleAlignment)
        {
            //throw new NotImplementedException();
        }
    }
}
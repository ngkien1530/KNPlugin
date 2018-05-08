using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Foundation;
using Sample.iOS.Renderers;
using Sample.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;

[assembly: ExportRenderer(typeof(AdvNavigationPage), typeof(AdvNavigationPageRenderer))]
namespace Sample.iOS.Renderers
{
    public class AdvNavigationPageRenderer: NavigationRenderer
    {
        public AdvNavigationPage KNNavigationPageElement => Element as AdvNavigationPage;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            KNNavigationPageElement.PropertyChanged += Element_PropertyChanged;

            UpdateToolbarContentPosition(AdvNavigationPage.GetIsBarOverlap(KNNavigationPageElement));     
        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.PropertyName);
            if (e.PropertyName == AdvNavigationPage.IsBarOverlapProperty.PropertyName && IsViewLoaded)
            {
                UpdateToolbarContentPosition(AdvNavigationPage.GetIsBarOverlap(KNNavigationPageElement));
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= Element_PropertyChanged;
            }
            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += Element_PropertyChanged;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
        }
       
        private void UpdateToolbarContentPosition(bool isOverlap)
        {
            //NavigationBar.Translucent = isOverlap;
        }
    }
}
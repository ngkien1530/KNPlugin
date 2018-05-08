using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Sample.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollView), typeof(CustomScrollViewRenderer))]
namespace Sample.iOS.Renderers
{
    public class CustomScrollViewRenderer: ScrollViewRenderer
    {
        public CustomScrollViewRenderer()
        {
            ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
            ContentInset = UIEdgeInsets.Zero;
            ScrollIndicatorInsets = UIEdgeInsets.Zero;
        }
    }
}
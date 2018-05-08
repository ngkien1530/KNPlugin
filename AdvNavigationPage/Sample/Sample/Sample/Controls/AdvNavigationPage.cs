using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sample.Controls
{
    public class AdvNavigationPage: NavigationPage
    {
        public AdvNavigationPage()
        {
        }

        public AdvNavigationPage(Page root) : base(root)
        {
        }

        public static readonly BindableProperty TitleAlignmentProperty =
            BindableProperty.Create(nameof(TitleAlignment), 
                typeof(TextAlignment), typeof(AdvNavigationPage), TextAlignment.Start);

        public static readonly BindableProperty IsBarOverlapProperty =
            BindableProperty.CreateAttached("IsBarOverlap",
                typeof(bool), typeof(AdvNavigationPage), false);

        public static readonly BindableProperty BarBackgroundOpacityProperty =
            BindableProperty.CreateAttached("BarBackgroundOpacity",
                typeof(double), typeof(AdvNavigationPage), 1.0);

        public new static readonly BindableProperty BarBackgroundColorProperty =
            BindableProperty.Create("BarBackgroundColor",
                typeof(Color), typeof(AdvNavigationPage), Color.Default);
        
        public new Color BarBackgroundColor
        {
            get => (Color)GetValue(BarBackgroundColorProperty);
            set => SetValue(BarBackgroundColorProperty, value);
        }

        public TextAlignment TitleAlignment
        {
            get => (TextAlignment) GetValue(TitleAlignmentProperty);
            set => SetValue(TitleAlignmentProperty, value);
        }

        public static bool GetIsBarOverlap(BindableObject view)
        {
            return (bool)view.GetValue(IsBarOverlapProperty);
        }

        public static void SetIsBarOverlap(BindableObject view, bool value)
        {
            view.SetValue(IsBarOverlapProperty, value);
        }

        public static double GetBarBackgroundOpacity(BindableObject view)
        {
            return (double)view.GetValue(BarBackgroundOpacityProperty);
        }

        public static void SetBarBackgroundOpacity(BindableObject view, double value)
        {
            view.SetValue(BarBackgroundOpacityProperty, value);
        }
    }
}

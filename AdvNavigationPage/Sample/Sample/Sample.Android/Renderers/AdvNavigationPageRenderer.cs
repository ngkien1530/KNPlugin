using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Sample.Controls;
using Sample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Color = Android.Graphics.Color;
using Orientation = Android.Widget.Orientation;
using TextAlignment = Xamarin.Forms.TextAlignment;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(AdvNavigationPage), typeof(AdvNavigationPageRenderer))]
namespace Sample.Droid.Renderers
{
    public class AdvNavigationPageRenderer : NavigationPageRenderer
    {
        protected Toolbar Toolbar;
        protected TextView TextViewTitle;
        protected LinearLayout LayoutParent;
        private Page CurrentPage { get; set; }
        private Page PreviousPage { get; set; }
        private Rectangle OriginalConainterArea => PageController.ContainerArea;

        public AdvNavigationPageRenderer(Context context) : base(context)
        {
        }

        public AdvNavigationPage KNNavigationPageElement => Element as AdvNavigationPage;
        IPageController PageController => Element as IPageController;

        protected override void SetupPageTransition(Android.Support.V4.App.FragmentTransaction transaction, bool isPush)
        {
            if (isPush)
            {
                if (Element?.Navigation?.NavigationStack?.Count >= 2)
                {
                    PreviousPage = Element?.Navigation?.NavigationStack[Element.Navigation.NavigationStack.Count - 2];
                    PreviousPage.PropertyChanged -= PagePropertyChanged;
                }

                CurrentPage = Element?.Navigation?.NavigationStack?.Last();
                if (CurrentPage != null)
                    CurrentPage.PropertyChanged += PagePropertyChanged;
            }
            else if (Element?.Navigation?.NavigationStack?.Count >= 2)
            {
                PreviousPage = Element?.Navigation?.NavigationStack?.Last();
                PreviousPage.PropertyChanged -= PagePropertyChanged;

                CurrentPage = Element?.Navigation?.NavigationStack[Element.Navigation.NavigationStack.Count - 2];
                if (CurrentPage != null)
                    CurrentPage.PropertyChanged += PagePropertyChanged;

            }

            Setup(CurrentPage);

            base.SetupPageTransition(transaction, isPush);
        }

        public override void OnViewAdded(View child)
        {
            base.OnViewAdded(child);

            if (child.GetType() == typeof(Toolbar))
            {
                Toolbar = (Toolbar)child;

                LayoutParent = new LinearLayout(Toolbar.Context)
                {
                    LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent),
                    Orientation = Orientation.Vertical
                };
                LayoutParent.SetGravity(GravityFlags.Center);

                TextViewTitle = new TextView(LayoutParent.Context)
                {
                    LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
                };
                TextViewTitle.SetTextAppearance(Resource.Style.TextAppearance_AppCompat_Widget_ActionBar_Title);

                LayoutParent.AddView(TextViewTitle);
                Toolbar.AddView(LayoutParent);
            }

            CurrentPage = Element?.Navigation?.NavigationStack?.Last();
            if (CurrentPage != null)
            {
                CurrentPage.PropertyChanged += PagePropertyChanged;
                Setup(CurrentPage);
            }
        }

        public override void OnViewRemoved(View child)
        {
            base.OnViewRemoved(child);

            if (Toolbar != null && PreviousPage != null)
            {
                PreviousPage.PropertyChanged -= PagePropertyChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == AdvNavigationPage.TitleAlignmentProperty.PropertyName)
            {
                UpdateBarTitleAlignment(KNNavigationPageElement.TitleAlignment);
            }
            if (e.PropertyName == AdvNavigationPage.BarBackgroundColorProperty.PropertyName)
            {
                UpdateBarBackgroundColor(KNNavigationPageElement.BarBackgroundColor);
            }
        }

        private void Setup(Page currentPage)
        {
            UpdateBarTitleText(currentPage.Title);
            UpdateBarTitleColor(KNNavigationPageElement.BarTextColor);
            UpdateBarTitleAlignment(KNNavigationPageElement.TitleAlignment);
            UpdateBarBackgroundColor(KNNavigationPageElement.BarBackgroundColor);
            UpdateBarBackgroundOpacity(AdvNavigationPage.GetBarBackgroundOpacity(currentPage));
            UpdateIsBarOverlapPosition(AdvNavigationPage.GetIsBarOverlap(CurrentPage));
        }

        private void PagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AdvNavigationPage.IsBarOverlapProperty.PropertyName)
            {
                UpdateIsBarOverlapPosition(AdvNavigationPage.GetIsBarOverlap(CurrentPage));
            }
            if (e.PropertyName == AdvNavigationPage.BarBackgroundOpacityProperty.PropertyName)
            {
                UpdateBarBackgroundOpacity(AdvNavigationPage.GetBarBackgroundOpacity(CurrentPage));
            }
            if (e.PropertyName == Page.TitleProperty.PropertyName)
            {
                UpdateBarTitleText(CurrentPage.Title);
            }
            if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName)
            {
                UpdateBarTitleColor(KNNavigationPageElement.BarTextColor);
            }
        }

        private void UpdateBarTitleColor(Xamarin.Forms.Color color)
        {
            TextViewTitle.SetTextColor(color.ToAndroid());
        }

        private void UpdateBarTitleText(string title)
        {
            TextViewTitle.Text = string.IsNullOrEmpty(title) ? string.Empty : title;
        }

        private void UpdateBarBackgroundColor(Xamarin.Forms.Color color)
        {
            Toolbar.BackgroundTintMode = null;
            Toolbar.BackgroundTintList = null;
            Toolbar.SetBackgroundColor(color.ToAndroid());
        }

        private void UpdateBarBackgroundOpacity(double opacity)
        {
            if (opacity > 1.0f)
                Toolbar?.Background.SetAlpha((int)(1.0f * 255));
            else if (opacity < 0)
                Toolbar?.Background.SetAlpha(0);
            else
                Toolbar?.Background.SetAlpha((int)(opacity * 255));
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            Setup(CurrentPage);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (AdvNavigationPage.GetIsBarOverlap(CurrentPage))
                SetContentBehindToolbar(l, t, r, b);
        }

        private void UpdateBarTitleAlignment(TextAlignment alignment)
        {
            if (!(TextViewTitle.LayoutParameters is LinearLayout.LayoutParams textViewTitleParams))
                return;
            switch (alignment)
            {
                case Xamarin.Forms.TextAlignment.Start:
                    textViewTitleParams.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
                    break;
                case Xamarin.Forms.TextAlignment.Center:
                    textViewTitleParams.Gravity = GravityFlags.Center;
                    Toolbar.SetContentInsetsAbsolute(0, Toolbar.ContentInsetStartWithNavigation);
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    textViewTitleParams.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
                    break;
            }
            
            TextViewTitle.LayoutParameters = textViewTitleParams;
        }

        private void UpdateIsBarOverlapPosition(bool getContentPosition)
        {
            RequestLayout();
        }

        private void SetContentBehindToolbar(int l, int t, int r, int b)
        {
            KNNavigationPageElement.ContainerArea =
                new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(b - t));

            for (var i = 0; i < ChildCount; i++)
            {
                var child = GetChildAt(i);

                if (!(child is Toolbar))
                {
                    child.Layout(0, 0, r, b);
                }
            }
        }
    }
}
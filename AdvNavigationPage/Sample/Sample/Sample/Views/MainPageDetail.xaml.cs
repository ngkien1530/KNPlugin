using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            OverlaySwitch.IsToggled = true;

            AdvNavigationPage.SetBarBackgroundOpacity(this, 0);
            AdvNavigationPage.SetIsBarOverlap(this, true);

            MainScrollView.Scrolled += MainScrollView_Scrolled;
        }

        private void MainScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Debug.WriteLine("Translation Y: " + MainScrollView.TranslationY);
            Debug.WriteLine("Scroll Y: " + MainScrollView.ScrollY);
            var opacity = MainScrollView.ScrollY / (HeaderImage.Height);

            AdvNavigationPage.SetBarBackgroundOpacity(this, MainScrollView.ScrollY < HeaderImage.Height ? opacity : 1.0);
        }

        public double CalculateOpacity(double scrollY, double headerHeight)
        {
            if(scrollY >= headerHeight)
                return 1.0;

            return scrollY / headerHeight;
            
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            AdvNavigationPage.SetIsBarOverlap(this, e.Value);
        }
    }
}
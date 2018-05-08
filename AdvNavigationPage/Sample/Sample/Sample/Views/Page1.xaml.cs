using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent ();
		    Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.SetIsNavigationBarTranslucent(this, true);
            AdvNavigationPage.SetBarBackgroundOpacity(this, 0.2);
        }
	}
}
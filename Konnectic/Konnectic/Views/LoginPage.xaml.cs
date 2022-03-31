using Konnectic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Konnectic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginViewModel ViewModel => (LoginViewModel)BindingContext;

		public LoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
		}
        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterAccount());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var success = await ViewModel.AttemptLogin();
            if (success) await Navigation.PushAsync(new HomePage());
        }

        private void BtnDebug_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new FlowerOfLifePage());
        }
    }
}
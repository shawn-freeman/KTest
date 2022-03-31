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
	public partial class RegisterAccount : ContentPage
	{
        public RegisterAccountViewModel ViewModel => (RegisterAccountViewModel)BindingContext;

		public RegisterAccount ()
		{
			InitializeComponent ();
		}

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            var success = await ViewModel.Register();

            if(success) await Navigation.PopToRootAsync();
        }
    }
}
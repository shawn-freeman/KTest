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
	public partial class FlowerOfLifePage : ContentPage
	{
        public FlowerOfLifeViewModel ViewModel => (FlowerOfLifeViewModel)BindingContext;

        public FlowerOfLifePage (FlowerOfLifeViewModel viewModel)
		{
            BindingContext = viewModel;

            InitializeComponent();
        }

        private void BtnToggleChat_Clicked(object sender, EventArgs e)
        {
            ViewModel.DebugMessage = DateTime.Now.ToString();
        }

        public async void BtnSend_Clicked(object sender, EventArgs e)
        {
            await ViewModel.SendMessage();
        }
    }
}
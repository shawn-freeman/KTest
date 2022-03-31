using KonnecticTestCoreUtility.Models.Transports;
using KonnecticTestCoreUtility.Models.Transports.Fol;
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
	public partial class FolSessionListingPage : ContentPage
	{
        public FolSessionListingViewModel ViewModel => (FolSessionListingViewModel)BindingContext;

        public FolSessionListingPage ()
		{
            BindingContext = new FolSessionListingViewModel();

			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            await ViewModel.InitFolSessionListing();
        }

        private void BtnFindUsers_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.InitUserSearchListing();
        }

        private async void BtnConnections_Clicked(object sender, System.EventArgs e)
        {
            await ViewModel.InitFolSessionListing();
        }

        private async void BtnSearchUsers_Clicked(object sender, EventArgs e)
        {
            await ViewModel.SearchUsers();
        }

        private async void LvFolSessionListing_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.IsBusy = true;
            var sessionInfo = (FolResponseMessage)((ListView)sender).SelectedItem;

            if (ViewModel.ErrorInfo.IsError)
            {
                ViewModel.IsBusy = false;
                return;
            }

            //TODO: Navigate to the Flower of Life grid
            await Navigation.PushAsync(new FlowerOfLifePage(await FlowerOfLifeViewModel.New(sessionInfo.FolSessionId)));

            ViewModel.IsBusy = false;
        }

        private async void LvUserSearchListing_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var userInfo = (UserInfoModel)((ListView)sender).SelectedItem;
            var session = await ViewModel.CreateSession(new FolCreationRequest() { requesterUserId = App.AccessToken.UserId, requestedUserId = userInfo.UserId });

            if (ViewModel.ErrorInfo.IsError) return;

            //Change back to SessionListing
            await ViewModel.InitFolSessionListing();
        }
    }
}
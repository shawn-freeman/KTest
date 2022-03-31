using KonnecticTestCoreUtility.Models.Transports;
using KonnecticTestCoreUtility.Models.Transports.UserChat;
using Konnectic.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Konnectic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserChatListingPage : ContentPage
	{
        public UserChatListingViewModel ViewModel => (UserChatListingViewModel)BindingContext;

		public UserChatListingPage ()
		{
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            await ViewModel.InitChatSessionView();
        }

        private async void LvUserChatSessionListing_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.IsBusy = true;
            var session = (UserChatSessionItem)((ListView)sender).SelectedItem;
            await Navigation.PushAsync(new ChatHub(await ChatHubViewModel.New(session)));
            ViewModel.IsBusy = false;
        }

        private async void BtnFindUsers_Clicked(object sender, System.EventArgs e)
        {
            await ViewModel.InitUserListView();
        }

        private async void BtnConnections_Clicked(object sender, System.EventArgs e)
        {
            await ViewModel.InitChatSessionView();
        }

        private async void LvUserListing_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var userInfo = (UserInfoModel)((ListView)sender).SelectedItem;
            var session = await ViewModel.CreateSession(new List<int>() {userInfo.UserId });

            if (ViewModel.ErrorInfo.IsError) return;

            //In this case the chat hub only cares about the session ID but this will likely have to change
            await Navigation.PushAsync(new ChatHub(await ChatHubViewModel.New(session)));
        }
    }
}
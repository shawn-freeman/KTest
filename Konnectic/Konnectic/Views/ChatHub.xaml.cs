using KonnecticTestCoreUtility.Models.Transports.UserChat;
using Konnectic.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Konnectic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatHub : ContentPage
    {
        public ChatHubViewModel ViewModel => (ChatHubViewModel)BindingContext;

        public ChatHub(ChatHubViewModel viewModel)
        {
            viewModel.OnConversationUpdated = OnConversationUpdated;
            BindingContext = viewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var item = lvConversation.ItemsSource.Cast<object>().LastOrDefault();

            if(item != null) lvConversation.ScrollTo(item, ScrollToPosition.End, false);
        }

        private void OnConversationUpdated(UserChatMessageInfo message)
        {
            lvConversation.ScrollTo(message, ScrollToPosition.End, true);
        }

        protected async void BtnSend_Clicked(object sender, EventArgs e)
        {
            await ViewModel.SendMessage();
        }
    }
}
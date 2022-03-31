using Konnectic.Views;
using System;
using System.Collections.ObjectModel;

namespace Konnectic.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_username)) _username = "";

                return _username;
            }
            set
            {
                SetProperty(ref _username, value);
            }
        }

        public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }

        public HomePageViewModel()
        {
            Username = App.AccessToken.Username;

            MenuItems = new ObservableCollection<HomePageMenuItem>(new[]
            {
                    new HomePageMenuItem { PageType = typeof(UserChatListingPage), Title = "Chat" },
                    new HomePageMenuItem { PageType = typeof(FolSessionListingPage), Title = "Flower of Life" },
                });
        }
    }

    public class HomePageMenuItem
    {
        public string Title { get; set; }

        public Type PageType { get; set; }
    }
}

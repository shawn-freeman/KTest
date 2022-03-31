
using KonnecticTestCoreUtility.Handlers;
using KonnecticTestCoreUtility.Models.EF;
using KonnecticTestCoreUtility.Models.Transports;
using KonnecticTestCoreUtility.Models.Transports.UserChat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Konnectic.ViewModels
{
    public class UserChatListingViewModel : BaseViewModel
    {
        private AccountHandler _accountHandler = new AccountHandler();
        private UserChatHandler _userChatHandler = new UserChatHandler();

        private bool _showUserListing;
        public bool ShowUserListing
        {
            get
            {
                return _showUserListing;
            }
            set
            {
                SetProperty(ref _showUserListing, value);
            }
        }

        private bool _showChatSessionListing;
        public bool ShowChatSessionListing
        {
            get
            {
                return _showChatSessionListing;
            }
            set
            {
                SetProperty(ref _showChatSessionListing, value);
            }
        }

        ObservableCollection<UserInfoModel> _usersListing;
        public ObservableCollection<UserInfoModel> UsersListing
        {
            get
            {
                if (_usersListing == null) _usersListing = new ObservableCollection<UserInfoModel>();

                return _usersListing;
            }
            set
            {
                SetProperty(ref _usersListing, value);
            }
        }

        ObservableCollection<UserChatSessionItem> _sessionListing;
        public ObservableCollection<UserChatSessionItem> SessionListing
        {
            get
            {
                if (_sessionListing == null) _sessionListing = new ObservableCollection<UserChatSessionItem>();

                return _sessionListing;
            }
            set
            {
                SetProperty(ref _sessionListing, value);
            }
        }

        public UserChatListingViewModel()
        {

        }

        public async Task InitChatSessionView()
        {
            IsBusy = true;
            ShowUserListing = false;

            var sessionListResult = await _userChatHandler.GetUserChatSessionListing(App.AccessToken.UserId);

            ErrorInfo = sessionListResult.Error;

            if(!ErrorInfo.IsError)
                SessionListing = new ObservableCollection<UserChatSessionItem>(sessionListResult.Result);

            ShowChatSessionListing = true;
            UsersListing.Clear();
            IsBusy = false;
        }

        public async Task InitUserListView()
        {
            IsBusy = true;
            ShowChatSessionListing = false;

            var userListResult = await _accountHandler.GetUsers();

            ErrorInfo = userListResult.Error;

            //Remove logged in user from the list and
            //any user that already has a session with the current user
            if (!ErrorInfo.IsError)
                UsersListing = new ObservableCollection<UserInfoModel>(
                    userListResult.Result.Where(u => 
                        u.UserId != App.AccessToken.UserId && 
                        !SessionListing.Any(sl => sl.SessionMembers.Any(smu => smu.UserId == u.UserId))
                    ));

            ShowUserListing = true;
            SessionListing.Clear();
            IsBusy = false;
        }

        public async Task<UserChatSessionItem> CreateSession(List<int> userIds)
        {
            IsBusy = true;
            
            var creationList = new List<int>(userIds);
            creationList.Add(App.AccessToken.UserId);

            var sessionResult = await _userChatHandler.CreateUserChatSession(creationList);

            ErrorInfo = sessionResult.Error;

            IsBusy = false;

            return sessionResult.Result;
        }
    }
}

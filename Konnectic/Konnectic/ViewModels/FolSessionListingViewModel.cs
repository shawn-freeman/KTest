using KonnecticTestCoreUtility.Handlers;
using KonnecticTestCoreUtility.Models.Transports;
using KonnecticTestCoreUtility.Models.Transports.Fol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnectic.ViewModels
{
    public class FolSessionListingViewModel : BaseViewModel
    {
        private UserHandler _userHandler = new UserHandler();
        private FolHandler _folHandler = new FolHandler();

        private string _searchFilter;
        public string SearchFilter
        {
            get
            {
                return _searchFilter;
            }
            set
            {
                SetProperty(ref _searchFilter, value);
            }
        }

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

        private bool _showFolSessionListing;
        public bool ShowFolSessionListing
        {
            get
            {
                return _showFolSessionListing;
            }
            set
            {
                SetProperty(ref _showFolSessionListing, value);
            }
        }

        ObservableCollection<UserInfoModel> _userSearchListing;
        public ObservableCollection<UserInfoModel> UserSearchListing
        {
            get
            {
                if (_userSearchListing == null) _userSearchListing = new ObservableCollection<UserInfoModel>();

                return _userSearchListing;
            }
            set
            {
                SetProperty(ref _userSearchListing, value);
            }
        }

        ObservableCollection<FolResponseMessage> _folSessionListing;
        public ObservableCollection<FolResponseMessage> FolSessionListing
        {
            get
            {
                if (_folSessionListing == null) _folSessionListing = new ObservableCollection<FolResponseMessage>();

                return _folSessionListing;
            }
            set
            {
                SetProperty(ref _folSessionListing, value);
            }
        }

        public FolSessionListingViewModel()
        {

        }

        public async Task InitFolSessionListing()
        {
            IsBusy = true;
            ShowUserListing = false;
            UserSearchListing.Clear();

            var sessionListResult = await _folHandler.GetFolSessionListing(App.AccessToken.UserId);

            ErrorInfo = sessionListResult.Error;

            if (!ErrorInfo.IsError)
                FolSessionListing = new ObservableCollection<FolResponseMessage>(sessionListResult.Result);

            ShowFolSessionListing = true;
            IsBusy = false;
        }

        public void InitUserSearchListing()
        {
            ShowFolSessionListing = false;
            ShowUserListing = true;
        }

        public async Task SearchUsers()
        {
            IsBusy = true;

            var userListResult = await _userHandler.SearchUsers(App.AccessToken.UserId, SearchFilter);

            ErrorInfo = userListResult.Error;

            //Remove logged in user from the list
            if (!ErrorInfo.IsError)
                UserSearchListing = new ObservableCollection<UserInfoModel>(userListResult.Result);

            IsBusy = false;
        }

        public async Task<FolResponseMessage> CreateSession(FolCreationRequest folCreationRequest)
        {
            IsBusy = true;
            
            var sessionResult = await _folHandler.CreateFolSession(folCreationRequest);

            ErrorInfo = sessionResult.Error;

            IsBusy = false;

            return sessionResult.Result;
        }
    }
}

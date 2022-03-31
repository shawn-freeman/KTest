using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KonnecticTestCoreUtility.Handlers
{
    public class AccountHandler : BaseHttpHandler
    {

        public AccountHandler (string baseAddressOverride = null) : base(baseAddressOverride)
        {
            
        }

        public async Task<ReturnResult<bool>> CreateUser(UserCreationModel creationModel)
        {
            var result = await PostAsync<UserCreationModel, bool>("api/Login/CreateUser", creationModel);

            return result;
        }

        public async Task<ReturnResult<UserInfoModel>> GetUser(string username, string password)
        {
            var result = await GetAsync<UserInfoModel>("api/Login/GetUser", $"?username={username}&password={password}");

            return result;
        }

        public async Task<ReturnResult<List<UserInfoModel>>> GetUsers()
        {
            var result = await GetAsync<List<UserInfoModel>>("api/Login/GetUsers");

            return result;
        }
    }
}

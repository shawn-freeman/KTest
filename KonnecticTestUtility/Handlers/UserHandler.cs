using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KonnecticTestCoreUtility.Handlers
{
    public class UserHandler : BaseHttpHandler
    {
        public UserHandler(string baseAddressOverride = null) : base(baseAddressOverride)
        {

        }

        public async Task<ReturnResult<List<UserInfoModel>>> SearchUsers(int userId, string filter)
        {
            var result = await GetAsync<List<UserInfoModel>>("api/User/SearchUsers", $"?userId={userId}&filter={filter}");

            return result;
        }
    }
}

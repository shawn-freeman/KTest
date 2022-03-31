using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.EF;
using KonnecticTestCoreUtility.Models.Transports;
using KonnecticTestCoreUtility.Models.Transports.UserChat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KonnecticTestCoreUtility.Handlers
{
    public class UserChatHandler : BaseHttpHandler
    {
        public UserChatHandler(string baseAddressOverride = null) : base(baseAddressOverride)
        {

        }

        public async Task<ReturnResult<UserChatSessionItem>> GetSessionInfo(Guid sessionId)
        {
            var result = await GetAsync<UserChatSessionItem>("api/UserChats/GetSessionInfo", $"?sessionId={sessionId}");

            return result;
        }

        public async Task<ReturnResult<UserChatSessionItem>> CreateUserChatSession(List<int> userIds)
        {
            var result = await PostAsync<List<int>, UserChatSessionItem>("api/UserChats/CreateUserChatSession", userIds);

            return result;
        }

        public async Task<ReturnResult<List<UserChatSessionItem>>> GetUserChatSessionListing(int userId)
        {
            var result = await GetAsync<List<UserChatSessionItem>>("api/UserChats/GetUserChatSessionListing", $"?userId={userId}");

            return result;
        }

        public async Task<ReturnResult<List<UserChatMessageInfo>>> GetUserChatSessionMessages(Guid sessionId)
        {
            var result = await GetAsync<List<UserChatMessageInfo>>("api/UserChats/GetUserChatSessionMessages", $"?sessionId={sessionId}");

            return result;
        }
    }
}

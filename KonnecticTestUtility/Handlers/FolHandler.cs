using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports.Fol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KonnecticTestCoreUtility.Handlers
{
    public class FolHandler : BaseHttpHandler
    {
        public FolHandler(string baseAddressOverride = null) : base(baseAddressOverride)
        {

        }

        public async Task<ReturnResult<FolResponseMessage>> CreateFolSession(FolCreationRequest creationRequest)
        {
            var result = await PostAsync<FolCreationRequest, FolResponseMessage>("api/FlowerOfLife/CreateFolSession", creationRequest);

            return result;
        }

        public async Task<ReturnResult<FolResponseMessage>> GetFolSession(Guid sessionId)
        {
            var result = await GetAsync<FolResponseMessage>("api/FlowerOfLife/GetFlowerSession", $"?sessionId={sessionId}");

            return result;
        }

        public async Task<ReturnResult<List<FolResponseMessage>>> GetFolSessionListing(int userId)
        {
            var result = await GetAsync<List<FolResponseMessage>>("api/FlowerOfLife/GetUserFlowerSessionListing", $"?userId={userId}");

            return result;
        }
    }
}

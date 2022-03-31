using KonnecticTestCoreUtility.Handlers;
using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports;
using System.Threading.Tasks;

namespace Konnectic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private AccountHandler _accountHandler = new AccountHandler();

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        

        private void Reset()
        {
            ErrorInfo = null;
        }

        public async Task<bool> AttemptLogin()
        {
            IsBusy = true;
            Reset();

            var result = await _accountHandler.GetUser(Username, Password);
            
            ErrorInfo = result.Error;

            App.AccessToken = result.Result;

            IsBusy = false;

            return result.Success;
        }
    }
}

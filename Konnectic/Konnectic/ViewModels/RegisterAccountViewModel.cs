using KonnecticTestCoreUtility.Handlers;
using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports;
using System.Threading.Tasks;

namespace Konnectic.ViewModels
{
    public class RegisterAccountViewModel : BaseViewModel
    {
        private AccountHandler _accountHandler = new AccountHandler();

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public async Task<bool> Register()
        {
            ErrorInfo = new ErrorInfo();
            IsBusy = true;

            var result = await _accountHandler.CreateUser(
                new UserCreationModel() {
                    Email = Email,
                    Username = Username,
                    Password = Password
            });

            ErrorInfo = result.Error;

            IsBusy = false;

            return !result.Error.IsError;
        }
    }
}

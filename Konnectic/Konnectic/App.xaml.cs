using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Konnectic.Services;
using Konnectic.Views;
using KonnecticTestCoreUtility.Models.Common;
using KonnecticTestCoreUtility.Models.Transports;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Konnectic
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public static double ScreenWidth;
        public static double ScreenHeight;

        public static UserInfoModel AccessToken;

        public App()
        {
            KonnecticTestAppSettings.KonnecticTestBaseUrl = "https://KonnecticTestCore.azurewebsites.net/";

            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using MobTracker.Client.Services.Interfaces;
using MobTracker.Client.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobTracker.Client
{
    public partial class App : Application
    {
        private IAuthenticationService _authenticationService = DependencyService.Get<IAuthenticationService>(DependencyFetchTarget.GlobalInstance);
        public App()
        {
            InitializeComponent();

            if (_authenticationService.IsAuthenticated)
            {
                MainPage = new NavigationPage(new ConnectPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

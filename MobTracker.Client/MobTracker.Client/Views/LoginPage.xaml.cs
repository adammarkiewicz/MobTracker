using MobTracker.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobTracker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
		private IAuthenticationService _authenticationService = DependencyService.Get<IAuthenticationService>(DependencyFetchTarget.GlobalInstance);

		public LoginPage()
        {
            InitializeComponent();
        }

		private async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			loginButton.IsEnabled = false;

			await _authenticationService.Authenticate();

			if (_authenticationService.IsAuthenticated)
			{
				Navigation.InsertPageBefore(new ConnectPage(), this);
				await Navigation.PopAsync();
			}
			else
			{
				loginButton.IsEnabled = true;
				messageLabel.Text = "Login failed";
			}
		}
	}
}
using MobTracker.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobTracker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectPage : ContentPage
    {
        private IApiService _apiService = DependencyService.Get<IApiService>(DependencyFetchTarget.GlobalInstance);

        public ConnectPage()
        {
            InitializeComponent();
        }

        private async void OnConnectButtonClicked(object sender, EventArgs args)
        {
            try
            {
                connectButton.IsEnabled = false;
                await _apiService.Connect();
                messageLabel.Text = "Connected";
            }
            catch (Exception)
            {
                connectButton.IsEnabled = true;
                messageLabel.Text = "Service not available";
            }
        }
    }
}
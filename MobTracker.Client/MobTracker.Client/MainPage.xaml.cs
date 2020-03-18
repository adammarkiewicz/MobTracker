using Microsoft.AspNetCore.SignalR.Client;
using MobTracker.Client.Services;
using MobTracker.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobTracker.Client
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        private ApiService _apiService;
        

        public MainPage()
        {
            InitializeComponent();

            _httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(_httpClientHandler);

            _apiService = DependencyService.Get<ApiService>(DependencyFetchTarget.GlobalInstance);

            /*
            _connection = new HubConnectionBuilder()
                    .WithUrl("https://10.0.2.2:44375/api", options =>
                    {
                        options.HttpMessageHandlerFactory = (_) => _httpClientHandler;
                    })
                    .Build();

            _connection.On("IntroduceYourself", async () =>
                await _connection.InvokeAsync("DeviceIntroduction", "device1"));

            _connection.On("GetLocationFromTracker", async () =>
                await _connection.InvokeAsync("ReceiveLocationFromTracker", "47.275175, 8.448372"));

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };*/
        }

        private async void OnConnectButtonClicked(object sender, EventArgs args)
        {
            try
            {
                connectButton.IsEnabled = false;
                await _apiService.Connect();
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", "Debugger.Log: " + ex.Message);
            }
            finally
            {
                connectButton.IsEnabled = true;
            }
        }

        private async void OnSendButtonClicked(object sender, EventArgs args)
        {
            try
            {
                //await _connection.InvokeAsync("ReceiveLocationFromTracker", "47.275175, 8.448372");
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", ex.Message);
            }
        }

        private async void OnTestHttpsButtonClicked(object sender, EventArgs args)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("https://10.0.2.2:44375/api/test");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                label.Text = responseBody;
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", ex.Message);
            }
        }

        private async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            try
            {
                LoginButton.IsEnabled = false;

                var authenticationService = DependencyService.Get<IAuthenticationService>(DependencyFetchTarget.GlobalInstance);
                var authenticationResult = await authenticationService.Authenticate();

                label.Text = authenticationResult.IdToken.Substring(0, 30);
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", ex.Message);
            }
            finally
            {
                LoginButton.IsEnabled = true;
            }
        }
    }
}

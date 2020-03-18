using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MobTracker.Client.Config;
using MobTracker.Client.Model;
using MobTracker.Client.Services;
using MobTracker.Client.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ApiService))]
namespace MobTracker.Client.Services
{
    public class ApiService : IApiService
    {
        //needed for https to work in dev environment
        private HttpClientHandler _httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        private IAuthenticationService _authenticationService =
            DependencyService.Get<IAuthenticationService>(DependencyFetchTarget.GlobalInstance);
        private HubConnection _connection;

        public async Task Connect()
        {
            if (_authenticationService.IsAuthenticated == false)
            {
                await _authenticationService.Authenticate();
            }

            _connection = new HubConnectionBuilder()
                    .WithUrl(ApiConfig.Url, options =>
                    {
                        //needed for https to work in dev environment
                        options.HttpMessageHandlerFactory = (_) => _httpClientHandler;

                        options.AccessTokenProvider = () => Task.FromResult(_authenticationService.AuthenticationResult.AccessToken);
                    })
                    .WithAutomaticReconnect()
                    .Build();

            InitializeDeviceApi();

            await _connection.StartAsync();
            await _connection.InvokeAsync("AddToGroup", ApplicationConfig.ApplicationType);
        }

        private void InitializeDeviceApi()
        {
            _connection.On("IntroduceYourself", async () =>
            {
                var deviceInfo = new DeviceInfo { Id = "ewqcqecqe", Brand = "Samsung", Model = "Galaxy" };
                await _connection.InvokeAsync("DeviceIntroduction", deviceInfo);
            });

            /*_connection.On<string, string>("IntroduceYourself2", (user, message) =>
            {
                //do something on your UI maybe?
            });*/
        }
    }
}

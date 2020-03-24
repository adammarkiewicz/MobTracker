using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MobTracker.Client.Config;
using MobTracker.Client.Model;
using MobTracker.Client.Services;
using MobTracker.Client.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Essentials;

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
        private IDeviceInfoService _deviceInfoService =
            DependencyService.Get<IDeviceInfoService>(DependencyFetchTarget.GlobalInstance);
        private CancellationTokenSource _trackingCancellationTokenSource;
        private HubConnection _connection;


        public async Task Connect()
        {
            if (_authenticationService.IsAuthenticated == false)
            {
                throw new Exception("Must be authenticated before connecting.");
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
            await _connection.InvokeAsync("IntroduceMe", _deviceInfoService.DeviceBadge);
        }

        private async Task SendLocationMarker()
        {
            var location = await GetLocation();
            var locationMarker = new LocationMarker
            {
                DeviceId = _deviceInfoService.DeviceBadge.Id,
                Colour = _deviceInfoService.DeviceBadge.Colour,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            await _connection.InvokeAsync("SendLocationMarker", locationMarker);
        }

        private async Task<Location> GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            return location;
        }

        private void InitializeDeviceApi()
        {
            _connection.On("IntroduceYourself", async () =>
            {
                await _connection.InvokeAsync("IntroduceMe", _deviceInfoService.DeviceBadge);
            });

            _connection.On("StartTracking", () =>
            {
                _trackingCancellationTokenSource = new CancellationTokenSource();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                StartTracking(_trackingCancellationTokenSource.Token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            });

            _connection.On("StopTracking", () => _trackingCancellationTokenSource.Cancel());
        }

        private async Task StartTracking(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await SendLocationMarker();
                await Task.Delay(3000);
            }
        }
    }
}

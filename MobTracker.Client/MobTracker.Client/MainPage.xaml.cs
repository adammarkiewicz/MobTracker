using Microsoft.AspNetCore.SignalR.Client;
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
        private HttpClient _httpClient;
        private HubConnection _connection;

        public MainPage()
        {
            InitializeComponent();

            _httpClient = new HttpClient();

            _connection = new HubConnectionBuilder()
                    .WithUrl("http://10.0.2.2:4000/api")
                    .Build();

            _connection.On("GetLocationFromTracker", async () =>
                await _connection.InvokeAsync("ReceiveLocationFromTracker", "47.275175, 8.448372"));

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }

        private async void OnConnectButtonClicked(object sender, EventArgs args)
        {
            try
            {
                connectButton.IsEnabled = false;
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", "Debugger.Log: " + ex.Message);
            }
        }

        private async void OnSendButtonClicked(object sender, EventArgs args)
        {
            try
            {
                await _connection.InvokeAsync("ReceiveLocationFromTracker", "47.275175, 8.448372");
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", ex.Message);
            }
        }

        private async void OnTestHttpButtonClicked(object sender, EventArgs args)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://10.0.2.2:4000/test");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                label.Text = responseBody.Substring(0, 50);
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "all", ex.Message);
            }
        }
    }
}

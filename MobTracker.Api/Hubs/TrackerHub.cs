using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobTracker.SignalrApi.Hubs
{
    public class TrackerHub : Hub
    {
        public async Task GetLocation()
        {
            await Clients.All.SendAsync("GetLocationFromTracker");
        }

        public async Task ReceiveLocationFromTracker(string location)
        {
            await Clients.All.SendAsync("ReceiveLocation", location);
        }
    }
}

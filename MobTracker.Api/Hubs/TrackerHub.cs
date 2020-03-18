using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MobTracker.Api.Constants;
using MobTracker.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MobTracker.SignalrApi.Hubs
{
    [Authorize]
    public class TrackerHub : Hub
    {
        public async Task AddToGroup(string applicationType)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var groupName = string.Concat(userId, applicationType);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task TrigerDevicesIntroduction()
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mobileGroupName = string.Concat(userId, ApplicationTypes.Mobile);

            await Clients.Group(mobileGroupName).SendAsync("IntroduceYourself");
        }

        public async Task DeviceIntroduction(Device device)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var adminUIGroupName = string.Concat(userId, ApplicationTypes.AdminUI);

            device.ConnectionId = Context.ConnectionId;

            await Clients.Group(adminUIGroupName).SendAsync("NewDevice", device);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var adminUIGroupName = string.Concat(userId, ApplicationTypes.AdminUI);

            await Clients.Group(adminUIGroupName).SendAsync("DeviceDisconnected", Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}

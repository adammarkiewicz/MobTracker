using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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

            if (applicationType == "mobile")
            {
                var adminUIGroupName = string.Concat(userId, "admin-ui");
                await Clients.Group(adminUIGroupName).SendAsync("NewDeviceConnected");
            }
        }

        public async Task TrigerIntroductionOfConnectedDevices()
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mobileGroupName = string.Concat(userId, "mobile");

            await Clients.Group(mobileGroupName).SendAsync("IntroduceYourself");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using DatingApi.Extensions;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Linq;

namespace DatingApi.SignalR
{
    [Authorize]
    public class PresenceHub : Hub  
    {
        private readonly PresenceTracker tracker;

        public PresenceHub(PresenceTracker tracker)
        {
            this.tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            await this.tracker.UserConnected(Context.UserIdentifier,Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUserName());
            var currentUsers = await tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetAllOnlineUsers",currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.tracker.UserDisconnected(Context.User.GetUserName(),Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOffline",Context.User.GetUserName());
            var users = await this.tracker.GetOnlineUsers();
            await Clients.All.SendAsync("GetAllOnlineUsers",users);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
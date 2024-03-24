using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using LebUpwork.Api.Hubs;
using Microsoft.AspNetCore.Authorization;

namespace LebUpwork.Api.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotification>
    {
        public override async Task OnConnectedAsync()
        {
           await Clients.Client(Context.ConnectionId).ReceiveNotification($"Thank you for connecting {Context.User?.Identity.Name}");
           await base.OnConnectedAsync();
        }

        //public async Task SendNotification(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveNotification", message);
        //}
    }
    
}
public interface INotification
{
    Task OnConnected();
    Task ReceiveNotification(string message);
}
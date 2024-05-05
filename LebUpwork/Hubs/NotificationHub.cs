using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using LebUpwork.Api.Hubs;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LebUpwor.core.Models;
using LebUpwork.Api.Repository;
using LebUpwork.Api.Interfaces;

namespace LebUpwork.Api.Hubs
{
    public class NotificationHub : Hub<INotification>    
    {
        private readonly IUserService _userService;
        public NotificationHub(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var id = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                    User User =await  _userService.GetUserById(int.Parse(id));
                    if (!string.IsNullOrEmpty(id))
                    {
                        await Clients.Client(Context.ConnectionId).ReceiveNotification($"Thank you for connecting {User.FirstName}");
                    }
                }
            }
        }
            //    await base.OnConnectedAsync();
            //}
            //public override async Task OnConnectedAsync() { 
            //var userIdClaim = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //await Clients.Client(Context.ConnectionId).ReceiveNotification($"Thank you for connecting {userIdClaim}");
            //    await base.OnConnectedAsync();
            //}

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
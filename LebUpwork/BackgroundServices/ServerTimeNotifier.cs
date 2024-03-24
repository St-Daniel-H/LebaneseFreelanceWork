using LebUpwork.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LebUpwork.Api.BackgroundServices
{
    public class ServerTimeNotifier : BackgroundService
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeNotifier> _logger;
        private readonly IHubContext<NotificationHub, INotification> _context;
        public ServerTimeNotifier(ILogger<ServerTimeNotifier> logger, IHubContext<NotificationHub, INotification> context)
        {
            _logger = logger;
            this._context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);
            while(!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var date = DateTime.Now;
                _logger.LogInformation("executing {Service}{Time} ",nameof(ServerTimeNotifier),date);
                _context.Clients.All.ReceiveNotification("hellloooo");
            }
        }
    }
}

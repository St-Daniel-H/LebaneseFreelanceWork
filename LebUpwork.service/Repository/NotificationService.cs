using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LebUpwork.service.Repository
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task CreateNewNotification(Notification newNotification)
        {

            await _unitOfWork.Notifications.AddAsync(newNotification);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllUserNotification(int userId)
        {
          return await _unitOfWork.Notifications.GetUserNotification(userId);
        }

        public async Task MarkNotificationAsRead(Notification newNotification)
        {
            newNotification.isRead = true;
            await _unitOfWork.CommitAsync();
        }
    }
}

using LebUpwor.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Interfaces
{
    public interface INotificationService
    {
        Task CreateNewNotification(Notification newNotification);
        Task MarkNotificationAsRead (Notification newNotification);
        Task <IEnumerable<Notification>> GetAllUserNotification (int userId);
    }
}

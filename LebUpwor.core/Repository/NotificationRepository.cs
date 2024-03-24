using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(UpworkLebContext context) : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
        public async Task<IEnumerable<Notification>> GetUserNotification(int userId)
        {
            return await UpworkLebContext.Notifications
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}

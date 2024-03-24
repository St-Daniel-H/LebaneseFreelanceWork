using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public int UserId { get; set; }
        public bool isRead { get; set; }

        public User? User { get; set; }
        public Notification()
        {
            isRead = false;
        }
    }
}

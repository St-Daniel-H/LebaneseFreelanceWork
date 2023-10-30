using LebUpwor.core.Models;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    public interface IMessageRepository : IRepositoryRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesBySenderId(int senderId);
        Task<IEnumerable<Message>> GetMessagesByReceiverId(int receiverId);
        Task<IEnumerable<Message>> GetUnreadMessagesForUser(int receiverId);
    }
}

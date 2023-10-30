using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        //Task<IEnumerable<Message>> GetMessagesBySenderId(int senderId);
        //Task<IEnumerable<Message>> GetMessagesByReceiverId(int receiverId);
        //Task<IEnumerable<Message>> GetUnreadMessagesForUser(int receiverId);
        public MessageRepository(UpworkLebContext context)
          : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
        public async Task<IEnumerable<Message>> GetMessagesBySenderId(int senderId)
        {
            return await UpworkLebContext.Messages
                .Where(message => message.SenderId == senderId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetMessagesByReceiverId(int receiverId)
        {
            return await UpworkLebContext.Messages
                .Where(message => message.ReceiverId == receiverId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetUnreadMessagesForUser(int receiverId)
        {
            return await UpworkLebContext.Messages
                .Where(message => message.ReceiverId == receiverId && !message.IsRead)
                .ToListAsync();
        }
    }
}

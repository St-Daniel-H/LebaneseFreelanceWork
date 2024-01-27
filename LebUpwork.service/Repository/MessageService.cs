using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Repository
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task DeleteMessage(Message message)
        {
             _unitOfWork.Messages.Remove(message);
             await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByReceiver(User user)
        {
            return await _unitOfWork.Messages.GetMessagesByReceiverId(user.UserId);
        }
        public async Task<IEnumerable<Message>> GetMessagesBySender(User user)
        {
            return await _unitOfWork.Messages.GetMessagesBySenderId(user.UserId);
            
        }
        public async Task<Message> SendMessage(Message message)
        {
            await _unitOfWork.Messages.AddAsync(message);
            return message;
        }
        public async Task<IEnumerable<Message>> GetUnreadMessages(User user)
        {
           return await _unitOfWork.Messages.GetUnreadMessagesForUser(user.UserId);
        }
    }
}

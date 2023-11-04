using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IMessageService
    {
        Task<Message> SendMessage(User from, User to, string content);
        Task<Message> DeleteMessage(int  messageId);
        Task<List<Message>> GetMessages(User user);    
    }
}

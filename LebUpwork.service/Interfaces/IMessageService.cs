using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface IMessageService
    {
        Task<Message> SendMessage(Message message);
        Task DeleteMessage(Message  message);
        Task<IEnumerable<Message>> GetMessagesBySender(User user);
        Task<IEnumerable<Message>> GetMessagesByReceiver(User user);
    }
}

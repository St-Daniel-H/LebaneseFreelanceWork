using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface ITokenHistoryService
    {
        Task<IEnumerable<TokenHistory>> GetTokenHistoryBySenderId(int userId);
        Task<IEnumerable<TokenHistory>> GetTokenHistoryByReceiverId(int userId);
        Task<IEnumerable<TokenHistory>> GetTokenHistoryByDate(string date);
        Task<TokenHistory> CreateTokenHistory(TokenHistory tokenHistory);

    }
}

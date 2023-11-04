using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface ITokenHistoryService
    {
        Task<TokenHistory> GetTokenHistoryOfUser(int userId);
        Task<TokenHistory> CreateTokenHistoryOfUser(int userId);
    }
}

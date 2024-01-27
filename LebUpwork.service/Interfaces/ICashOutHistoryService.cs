using LebUpwor.core.Models;

namespace LebUpwork.Api.Interfaces
{
    public interface ICashOutHistoryService
    {
        Task<IEnumerable<CashOutHistory>> GetCashOutHistoryOfUser(int userId);
        Task<CashOutHistory> CreateCashOutHistory(CashOutHistory cashOutHistory);
    }
}

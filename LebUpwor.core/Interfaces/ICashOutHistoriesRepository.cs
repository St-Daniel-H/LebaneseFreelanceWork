using LebUpwor.core.Models;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LebUpwor.core.Interfaces
{
    public interface ICashOutHistoriesRepository : IRepositoryRepository<CashOutHistory>
    {
        Task<IEnumerable<CashOutHistory>> GetAllCashOutHistoryWithUserId(int userId);
        Task<IEnumerable<CashOutHistory>> GetAllCashOutHistory();
    }
}

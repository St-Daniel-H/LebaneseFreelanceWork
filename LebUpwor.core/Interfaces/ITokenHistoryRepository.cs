using LebUpwor.core.Models;
using startup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    public interface ITokenHistoryRepository : IRepositoryRepository<TokenHistory>
    {
        Task<IEnumerable<TokenHistory>> GetTokenHistoryByUserId(int userId);
        Task<IEnumerable<TokenHistory>> GetTokenHistoryByDate(string date);
    }
}

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
        //public required int SenderId { get; set; }
        //public required int ReceiverId { get; set; }
        Task<IEnumerable<TokenHistory>> GetTokenHistoryBySenderId(int SenderId);
        Task<IEnumerable<TokenHistory>> GetTokenHistoryByReceiverId(int ReceiverId);

        Task<IEnumerable<TokenHistory>> GetTokenHistoryByDate(string date);
    }
}

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
    public class CashOutHistoryRepository : Repository<CashOutHistory>, ICashOutHistoryRepository

    {
        //Task<IEnumerable<CashOutHistory>> GetAllCashOutHistoryWithUserId(int userId);
        //Task<IEnumerable<CashOutHistory>> GetAllCashOutHistory();
        public CashOutHistoryRepository(UpworkLebContext context)
         : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
        public async Task<IEnumerable<CashOutHistory>> GetAllCashOutHistoryWithUserId(int userId)
        {
            return await UpworkLebContext.CashOutHistories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}

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
    public class TokenHistoryRepository : Repository<TokenHistory>, ITokenHistoryRepository
    {
        public TokenHistoryRepository(UpworkLebContext context)
           : base(context)
        { }

        //Task<IEnumerable<TokenHistory>> GetTokenHistoryByUserId(int userId);
        //Task<IEnumerable<TokenHistory>> GetTokenHistoryByDate(string date);
        public async Task<IEnumerable<TokenHistory>> GetAllTokenHistories()
        {
            return await UpworkLebContext.TokenHistories
                  .ToListAsync();
        }
        public async Task<IEnumerable<TokenHistory>> GetTokenHistoryByDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                return await UpworkLebContext.TokenHistories
                    .Where(history => history.Date.Date == parsedDate.Date) // Assuming history.Date is a DateTime property
                    .ToListAsync();
            }
            else
            {
                // Handle invalid date format (return empty list or throw an exception)
                return new List<TokenHistory>(); // Or you can throw an exception here
            }
        }
        public async Task<IEnumerable<TokenHistory>> GetTokenHistoryByReceiverId(int userId)
        {
            return await UpworkLebContext.TokenHistories
                 .Where(history => history.ReceiverId == userId)
                    .ToListAsync();   
        }
        public async Task<IEnumerable<TokenHistory>> GetTokenHistoryBySenderId(int userId)
        {
            return await UpworkLebContext.TokenHistories
                 .Where(history => history.SenderId == userId)
                    .ToListAsync();
        }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
    }
}

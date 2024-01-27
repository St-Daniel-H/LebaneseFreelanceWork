using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Repository
{
    public class CashOutHistoryService : ICashOutHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CashOutHistoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CashOutHistory>> GetCashOutHistoryOfUser(int userId)
        {
            return await _unitOfWork.CashOutHistories.GetAllCashOutHistoryWithUserId(userId);
        }
        public async Task<CashOutHistory> CreateCashOutHistory(CashOutHistory cashOutHistory)
        {
            await _unitOfWork.CashOutHistories.AddAsync(cashOutHistory);
            await _unitOfWork.CommitAsync();
            return cashOutHistory;
        }
    }
}

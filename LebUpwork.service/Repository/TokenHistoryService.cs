using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;

namespace LebUpwork.Api.Repository
{
    public class TokenHistoryService : ITokenHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TokenHistoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<TokenHistory> CreateTokenHistory(TokenHistory tokenHistory)
        {
            await _unitOfWork.TokenHistories.AddAsync(tokenHistory);
            await _unitOfWork.CommitAsync();
            return tokenHistory;
        }
        public async Task<IEnumerable<TokenHistory>> GetTokenHistoryByReceiverId(int userId)
        {
            return await _unitOfWork.TokenHistories.GetTokenHistoryByReceiverId(userId);
        }
        public async Task<IEnumerable<TokenHistory>> GetTokenHistoryBySenderId(int userId)
        {
            return await _unitOfWork.TokenHistories.GetTokenHistoryBySenderId(userId);
        }
        public async Task<IEnumerable<TokenHistory>> GetTokenHistoryByDate(string date)
        {
            return await _unitOfWork.TokenHistories.GetTokenHistoryByDate(date);
        }

    }
}

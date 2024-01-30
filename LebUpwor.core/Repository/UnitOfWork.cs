

using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;

namespace LebUpwor.core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UpworkLebContext _context;
        private UserRepository _userRepository;
        private JobRepository _jobRepository;
        private RoleRepository _roleRepository;
        private AppliedToTaskRepository _appliedToRepository;
        private CashOutHistoryRepository _cashoutRepository;
        private TokenHistoryRepository _tokenHRepository;
        private MessageRepository _messageRepository;

        public UnitOfWork(UpworkLebContext context)
        {
            this._context = context;
        }

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);
        public IJobRepository Jobs => _jobRepository = _jobRepository ?? new JobRepository(_context);
        public IRoleRepository Roles => _roleRepository = _roleRepository ?? new RoleRepository(_context);
        public IAppliedToTaskRepository AppliedToTasks => _appliedToRepository = _appliedToRepository ?? new AppliedToTaskRepository(_context);
        public ICashOutHistoryRepository CashOutHistories => _cashoutRepository = _cashoutRepository ?? new CashOutHistoryRepository(_context);
        public ITokenHistoryRepository TokenHistories => _tokenHRepository = _tokenHRepository ?? new TokenHistoryRepository(_context);
        public IMessageRepository Messages => _messageRepository = _messageRepository ?? new MessageRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
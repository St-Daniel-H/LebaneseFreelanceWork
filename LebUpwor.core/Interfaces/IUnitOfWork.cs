using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IJobRepository Jobs { get; }
        IRoleRepository Roles { get; }
        IMessageRepository Messages { get; }

        ICashOutHistoryRepository CashOutHistories { get; }
        IAppliedToTaskRepository AppliedToTasks { get; }
        ITokenHistoryRepository TokenHistories { get; }


        Task<int> CommitAsync();
    }
}

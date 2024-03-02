using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IJobRepository Jobs { get; }
        IRoleRepository Roles { get; }
        IMessageRepository Messages { get; }

        ICashOutHistoryRepository CashOutHistories { get; }
        IAppliedToTaskRepository AppliedToTasks { get; }
        ITokenHistoryRepository TokenHistories { get; }
        ITagRepository Tags { get; }
        IReportRepository Reports { get; }
        INewJobRepository NewJobs { get; }

        Task<int> CommitAsync();
    }
}

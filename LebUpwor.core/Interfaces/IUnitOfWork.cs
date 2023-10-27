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
        IJobsRepository Jobs { get; }
        IRolesRepository Roles { get; }
        IMessagesRepository Messages { get; }

        ICashOutHistoriesRepository CashOutHistories { get; }
        IAppliedToTasksRepository AppliedToTasks { get; }
        ITokenHistoryRepository TokenHistories { get; }


        Task<int> CommitAsync();
    }
}

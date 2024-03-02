using LebUpwor.core.DTO;
using LebUpwor.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Interfaces
{
    public interface INewJobService
    {
        Task CreateNewJob(NewJob newjob);
        Task DeleteNewJob(NewJob newjob);
        Task<NewJobDTO> getJobTrackerByJobId(int jobId);
         Task CommitChanges();
    }
}

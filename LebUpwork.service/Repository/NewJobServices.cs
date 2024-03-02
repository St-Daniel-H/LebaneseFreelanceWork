using LebUpwor.core.DTO;
using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwork.service.Repository
{
    public class NewJobServices : INewJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewJobServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task CreateNewJob(NewJob newjob)
        {
            await _unitOfWork.NewJobs.AddAsync(newjob);
            await _unitOfWork.CommitAsync();
        }

        public Task DeleteNewJob(NewJob newjob)
        {
            throw new NotImplementedException();
        }
        public async Task<NewJobDTO> getJobTrackerByJobId(int jobId)
        {
            return await _unitOfWork.NewJobs.GetJobTrackerByIds(jobId);
        }
        public async Task CommitChanges()
        {
            await _unitOfWork.CommitAsync();
        }
    }
}

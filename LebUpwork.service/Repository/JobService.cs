using LebUpwor.core.DTO;
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
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Job> GetJobById(int jobId)
        {
            return await _unitOfWork.Jobs.GetJobById(jobId);
        }
        public async Task<IEnumerable<JobDTO>> GetJobsWithTag(ICollection<string> tagStrings, int skip, int pageSize)
        {
            return await _unitOfWork.Jobs.GetJobsWithTag(tagStrings, skip,pageSize);
        }
        public async Task<Job> CreateJob(Job job) {
            await _unitOfWork.Jobs.AddAsync(job);
            await _unitOfWork.CommitAsync();
            return job;
        }
        public async Task<Job> UpdateJob(Job jobToUpdate, Job job) {

            jobToUpdate.Offer = job.Offer;
            jobToUpdate.Description = job.Description;
            jobToUpdate.Title = job.Title;
            jobToUpdate.IsCompleted = job.IsCompleted;
            jobToUpdate.FinishedByUser = job.FinishedByUser;

            await _unitOfWork.CommitAsync();
            return jobToUpdate;
        }
        public async Task DeleteJob(Job job) {
            _unitOfWork.Jobs.Remove(job);
            await _unitOfWork.CommitAsync();
        }
        public async Task CommitChanges()
        {
            await _unitOfWork.CommitAsync();
        }
        //public  Task<IEnumerable<Job>> GetJobsWithTag(ICollection<Tag> tags, int skip, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

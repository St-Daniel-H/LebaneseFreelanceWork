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
            return await _unitOfWork.Jobs.GetJobsWithTag(tagStrings, skip, pageSize);
        }
       public async Task<IEnumerable<JobDTO>> GetJobsWithKeyword(string keyword, int skip, int pageSize)
        {
            return await _unitOfWork.Jobs.GetJobsWithKeyword(keyword, skip, pageSize);
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
           // jobToUpdate.SelectedUser = job.SelectedUser;
            
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

        public async Task<IEnumerable<JobDTO>> GetJobsPostedByUser(int userId)
        {

            return await _unitOfWork.Jobs.GetAllJobsPostedByUser(userId);
        }

        public async Task<IEnumerable<JobDTO>> GetJobFinishedByUser(int userId)
        {
            return await _unitOfWork.Jobs.GetAllJobsFinishedByUser(userId);
        }
        public async Task<IEnumerable<JobDTO>> GetFinishedJobPostedByUser(int userId)
        {
            return await _unitOfWork.Jobs.GetAllFinishedJobsPostedByUser(userId);
        }
        public async Task<JobWithAppliedUsersDTO> GetJobWithAppliedUsers(int userId)
        {
            return await _unitOfWork.Jobs.GetJobByIdIncludeAppliedToTasks(userId);
        }
        //public  Task<IEnumerable<Job>> GetJobsWithTag(ICollection<Tag> tags, int skip, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

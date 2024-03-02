using LebUpwor.core.DTO;
using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace LebUpwor.core.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        //Task<IEnumerable<Job>> GetAllJobs();
        //Task<IEnumerable<Job>> GetJobsWithKeyword(string keywoard);
        //Task<IEnumerable<Job>> GetJobById(int jobId);
        //Task<IEnumerable<Job>> GetAllJobsPostedByUser(int userId);
        //Task<IEnumerable<Job>> GetAllJobsFinishedByUser(int userId);
        //Task<IEnumerable<Job>> GetAllFinishedJobsPostedByUser(int userId);

        public JobRepository(UpworkLebContext context)
          : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }
        public async Task<Job> GetJobById(int Jobid)
        {
            return await UpworkLebContext.Jobs
                 .Where(job => job.JobId == Jobid)
                 .Include(j => j.Tags)
                .SingleOrDefaultAsync();
        }
        public async Task<JobWithAppliedUsersDTO> GetJobByIdIncludeAppliedToTasks(int Jobid)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await UpworkLebContext.Jobs
                 .Where(job => job.JobId == Jobid)
                 .Select(j => new JobWithAppliedUsersDTO
                 {
                     JobId= j.JobId,
                     Title= j.Title,
                     Description= j.Description,
                     Offer= j.Offer,
                     PostedDate = j.PostedDate,
                     User = new UserDTO
                     {
                         UserId = j.UserId,
                         FirstName = j.User.FirstName,
                         LastName = j.User.LastName,
                         ProfilePicture = j.User.ProfilePicture
                     },
                     AppliedUsers = j.AppliedUsers.Select(appliedUser => new AppliedUsersDTO
                     {
                         //AppliedToTaskId = appliedUser.AppliedToTaskId,
                         AppliedDate = appliedUser.AppliedDate,
                         JobId = appliedUser.JobId,
                         UserId = appliedUser.UserId,
                         User = new UserDTO
                         {
                             UserId = j.User.UserId,
                             FirstName = j.User.FirstName,
                             LastName = j.User.LastName,
                             ProfilePicture = j.User.ProfilePicture
                         }, // Assuming AppliedUser has a navigation property named User of type UserDTO
                     }).ToList()

                 })
                .SingleOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
        public async Task<IEnumerable<JobDTO>> GetJobsWithTag(ICollection<string> tagStrings, int skip, int pageSize)
        {
            return await UpworkLebContext.Jobs
                 .Where(j => j.IsCompleted == false)
                .Where(j => j.Tags.Any(t => tagStrings.Contains(t.TagName)))
                .Skip(skip)
                .Take(pageSize)
                .Select(j => new JobDTO
                {       // Map only the necessary properties from User entity
                    JobId = j.JobId,
                    Title = j.Title,
                    Description = j.Description,
                    Offer = j.Offer,
                    PostedDate = j.PostedDate,
                    SelectCount = j.SelectCount,
                    User = new UserDTO
                    {
                        UserId = j.User.UserId,
                        FirstName = j.User.FirstName,
                        LastName = j.User.LastName,
                        ProfilePicture = j.User.ProfilePicture
                    },
                    Tags = (ICollection<TagDTO>)j.Tags.Select(n => new TagDTO { TagName = n.TagName })
                })
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<JobDTO>> GetJobsWithKeyword(string keyword, int skip, int pageSize)
        {
            return await UpworkLebContext.Jobs
            .Where(j => j.IsCompleted == false)
            .Where(j => j.Title.Contains(keyword) || j.Description.Contains(keyword))
                .Skip(skip)
                .Take(pageSize)
                .Select(j => new JobDTO
                {       // Map only the necessary properties from User entity
                    JobId = j.JobId,
                    Title = j.Title,
                    Description = j.Description,
                    Offer = j.Offer,
                    PostedDate = j.PostedDate,
                    User = new UserDTO
                    {
                        UserId = j.User.UserId,
                        FirstName = j.User.FirstName,
                        LastName = j.User.LastName,
                        ProfilePicture = j.User.ProfilePicture
                    },
                    Tags = (ICollection<TagDTO>)j.Tags.Select(n => new TagDTO { TagName = n.TagName })
                })
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await UpworkLebContext.Jobs
              .ToListAsync();
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsFinishedByUser(int userId)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.SelectedUserId == userId && j.IsCompleted==true)
                .Select(j => new JobDTO
                {
                    JobId = j.JobId,
                    Title = j.Title,
                    Description = j.Description,
                    Offer = j.Offer,
                    PostedDate = j.PostedDate,
                    User = new UserDTO
                    {
                        UserId = j.User.UserId,
                        FirstName = j.User.FirstName,
                        LastName = j.User.LastName,
                        ProfilePicture = j.User.ProfilePicture
                    },
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<JobDTO>> GetAllFinishedJobsPostedByUser(int userId)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.UserId == userId && j.IsCompleted == true)
                                .Select(j => new JobDTO
                                {
                                    JobId = j.JobId,
                                    Title = j.Title,
                                    Description = j.Description,
                                    Offer = j.Offer,
                                    PostedDate = j.PostedDate,
                                    User = new UserDTO
                                    {
                                        UserId = j.User.UserId,
                                        FirstName = j.User.FirstName,
                                        LastName = j.User.LastName,
                                        ProfilePicture = j.User.ProfilePicture
                                    },
                                }).ToListAsync();
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsPostedByUser(int userId)
        {
            return await UpworkLebContext.Jobs
                .Where(j => j.UserId == userId)
                     .Select(j => new JobDTO
                     {
                         JobId = j.JobId,
                         Title = j.Title,
                         Description = j.Description,
                         Offer = j.Offer,
                         PostedDate = j.PostedDate,
                         User = new UserDTO
                         {
                             UserId = j.User.UserId,
                             FirstName = j.User.FirstName,
                             LastName = j.User.LastName,
                             ProfilePicture = j.User.ProfilePicture
                         },
                     }).ToListAsync();
        }

        //Task<JobDTO> GetJobByIdIncludeAppliedToTasks(int Jobid)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

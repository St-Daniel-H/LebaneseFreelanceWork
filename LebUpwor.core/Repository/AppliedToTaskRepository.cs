using LebUpwor.core.DTO;
using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using Microsoft.EntityFrameworkCore;
using startup.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Repository
{
    public class AppliedToTaskRepository : Repository<AppliedToTask>, IAppliedToTaskRepository
    {
        //Task<IEnumerable<AppliedToTask>> GetAllUsersWithTaskId(int taskId);
        //Task<IEnumerable<AppliedToTask>> GetAllJobsWithUserId(int userId);
        public AppliedToTaskRepository(UpworkLebContext context)
        : base(context)
        { }
        private UpworkLebContext UpworkLebContext
        {
            get { return Context as UpworkLebContext; }
        }

        public async Task<IEnumerable<AppliedUsersDTO>> GetAllUsersWithTaskId(int taskId)
        {
            return await UpworkLebContext.AppliedToTasks
                .Where(a => a.JobId == taskId)
                .Select(appliedUser => new AppliedUsersDTO
                {
                    // AppliedToTaskId = appliedUser.AppliedToTaskId,
                    AppliedDate = appliedUser.AppliedDate,
                    JobId = appliedUser.JobId,
                    UserId = appliedUser.UserId,
                    SelectedUserId = appliedUser.Job.SelectedUserId ?? 0,
                    PostedDate = appliedUser.Job.PostedDate,
                    User = new UserDTO
                    {
                        UserId = appliedUser.User.UserId,
                        FirstName = appliedUser.User.FirstName,
                        LastName = appliedUser.User.LastName,
                        ProfilePicture = appliedUser.User.ProfilePicture
                    }, // Assuming AppliedUser has a navigation property named User of type UserDTO
                }).ToListAsync();
        }
        public async Task<IEnumerable<AppliedUserWithJobDTO>> GetAllJobsWithUserId(int userId)
        {
            return await UpworkLebContext.AppliedToTasks
                .Where(a => a.UserId == userId)
                                .Select(appliedUser => new AppliedUserWithJobDTO
                                {
                                    //  AppliedToTaskId = appliedUser.AppliedToTaskId,
                                    AppliedDate = appliedUser.AppliedDate,
                                    JobId = appliedUser.JobId,
                                    UserId = appliedUser.UserId,
                                    Job = new JobDTO
                                    {
                                        User = new UserDTO{
                                            UserId = appliedUser.Job.User.UserId,
                                            FirstName = appliedUser.Job.User.FirstName,
                                            LastName = appliedUser.Job.User.LastName,
                                            ProfilePicture = appliedUser.Job.User.ProfilePicture,
                                        },
                                        Title = appliedUser.Job.Title,
                                        Description = appliedUser.Job.Description,
                                        Offer = appliedUser.Job.Offer,
                                        PostedDate = appliedUser.Job.PostedDate,
                                        Tags = (ICollection<TagDTO>)appliedUser.Job.Tags.Select(n => new TagDTO { TagName = n.TagName })
                                    } 
                                })
                                .OrderByDescending(j=> j.AppliedDate)
                                .ToListAsync();
        }
    
    }
}

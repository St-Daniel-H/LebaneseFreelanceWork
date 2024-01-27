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
    public class AppliedToTaskService: IAppliedToTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppliedToTaskService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<AppliedToTask>> GetUsersAppliedByTaskId(int id)
        {
            return await _unitOfWork.AppliedToTasks.GetAllUsersWithTaskId(id);
        }

        public async Task<IEnumerable<AppliedToTask>> GetJobsApplied(int userId)
        {
            return await _unitOfWork.AppliedToTasks.GetAllJobsWithUserId(userId);
        }
        public async Task<AppliedToTask> CreateAppliedToTask(AppliedToTask taskapp)
        {
            await _unitOfWork.AppliedToTasks.AddAsync(taskapp);
            await _unitOfWork.CommitAsync();
            return taskapp;
        }

    }
}

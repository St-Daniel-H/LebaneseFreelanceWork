using AutoMapper;
using Hangfire;
using LebUpwor.core.Interfaces;
using LebUpwor.core.Models;
using LebUpwork.Api.Controllers;
using LebUpwork.Api.Interfaces;
using LebUpwork.service.Interfaces;

namespace LebUpwork.Api.CronJobs
{
    //this is meant to deduct the tokens from the user when the post is up for 24 hours!
    public class JobPost24HourMark
    {
        //private readonly IJobService _jobService;
        private readonly IUserService _userService;
        private readonly INewJobService _newJobService;
        public JobPost24HourMark(INewJobService newJobService, IUserService userService)
        {
          //  this._jobService = jobService;
            this._userService = userService;
            this._newJobService = newJobService;
        }
        public async Task YourHourlyFunction()
        {
            Console.WriteLine("job started");
            DateTime currentTime = DateTime.Now;
            var jobs = await _newJobService.GetAllJobTracks();
            foreach (var job in jobs)
            {
                if (job.Date == null) continue;

                TimeSpan? timeDifference = currentTime - job.Date;

                if (timeDifference.HasValue && timeDifference.Value.TotalHours > 24)
                {
                    var newJob = await _newJobService.getJobTrackerByJobId(job.JobId); // Retrieve NewJob record using JobId
                    if (newJob != null)
                    {
                        newJob.User.Token -= job.Job.Offer;
                        await _userService.CommitChanges();
                        await _newJobService.DeleteNewJob(newJob);
                        Console.WriteLine("Post deleted" + newJob.User.UserId);
                    }
                }
            }

        }

        public void ConfigureHangfire()
        {
            // Schedule the function to run every hour
            RecurringJob.AddOrUpdate("hourly-job", () => YourHourlyFunction(), "0 * * * *");
        }
    }
}

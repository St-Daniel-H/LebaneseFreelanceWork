using AutoMapper;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using LebUpwork.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LebUpwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppliedToTaskController : ControllerBase
    {
     

            private readonly IMapper _mapper;
            private readonly IJobService _jobService;
            private readonly IUserService _userService;
            private readonly IAppliedToTaskService _appliedToTaskService;
            private readonly ILogger<JobController> _logger;
            public AppliedToTaskController(IAppliedToTaskService appliedtotaskservice, IUserService userService, IJobService jobService, IMapper mapper, ILogger<JobController> logger)
            {
                this._mapper = mapper;
                this._jobService = jobService;
                this._userService = userService;
                this._logger = logger;
                this._appliedToTaskService = appliedtotaskservice;
            }
            [HttpGet("ApplyToJob")]
            [Authorize]
            public async Task<IActionResult> ApplyToJob(int JobId)
            {
                try
                {
                    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                    if (userIdClaim == null)
                    {
                        return Unauthorized("Unauthorized");
                    }

                    string userId = userIdClaim.Value;
                    var user = await _userService.GetUserById(int.Parse(userId));
                    if (user == null)  return BadRequest("Invalid User");

                    var job = await _jobService.GetJobWithAppliedUsers(JobId);
                    if (job == null) return BadRequest("Invalid Job");
                    bool userExistsInAppliedUsers = job.AppliedUsers.Any(appliedUser => appliedUser.UserId == int.Parse(userId));
                    if (userExistsInAppliedUsers)
                        return BadRequest("User already applied to this job");

                    AppliedToTask appliedtotask = new AppliedToTask
                    {
                        JobId = job.JobId,
                        UserId =user.UserId,
                        AppliedDate = DateTime.Now,                       
                    };
                    await _appliedToTaskService.CreateAppliedToTask(appliedtotask);
                    return Ok("Successfully Applied to the Job");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        [HttpGet("GetUsersAppliedByTaskId")]
        [Authorize]
        public async Task<IActionResult> GetUsersAppliedByTaskId(int JobId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized");
                }

                string userId = userIdClaim.Value;
                var user = await _userService.GetUserById(int.Parse(userId));
                if (user == null) return BadRequest("Invalid User");

                var job = await _jobService.GetJobWithAppliedUsers(JobId);
                if (job == null) return BadRequest("Invalid Job");
                if(job.User.UserId != int.Parse(userId)) 
                    return BadRequest("Unauthorized"); 
                var appliedUsers = await _appliedToTaskService.GetUsersAppliedByTaskId(JobId);
                
                return Ok(appliedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    
}

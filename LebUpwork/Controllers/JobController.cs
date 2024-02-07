using AutoMapper;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Settings;
using LebUpwork.Api.Validators;
using LebUpwork.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using LebUpwork.Api.Resources;
using LebUpwor.core.Models;
using LebUpwork.Api.Resources.Save;
using Microsoft.Extensions.Logging;
using LebUpwork.Api.Resources.Update;

namespace LebUpwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly IJobService _jobService;
        private readonly IUserService _userService;
        private readonly ILogger<JobController> _logger;

        public JobController(ITagService tagService,IUserService userService, IJobService jobService, IMapper mapper, ILogger<JobController> logger)
        {
            this._mapper = mapper;
            this._jobService = jobService;
            this._tagService = tagService;
            this._userService = userService;
            this._logger = logger;
        }
        [HttpGet("GetJobsWithSimilarTag")]
        [Authorize]
        public async Task<IActionResult> GetJobsWithSimilarTag(int skip, int pageSize)
        {
            try
            {

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized");
                }

                string userId = userIdClaim.Value;
                var user = await _userService.GetUserByIdWithTags(int.Parse(userId));
                if (user == null)
                {
                    return BadRequest("Invalid User");
                }
                var tagStrings = user.Tags.Select(t => t.TagName).ToList();
                var jobs = await _jobService.GetJobsWithTag(tagStrings, skip, pageSize);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetJobsWithKeywors")]
        [Authorize]
        public async Task<IActionResult> GetJobsWithSimilarTag(int skip, int pageSize,string keyword)
        {
            try
            {

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized");
                }

                string userId = userIdClaim.Value;
                var user = await _userService.GetUserByIdWithTags(int.Parse(userId));
                if (user == null)
                {
                    return BadRequest("Invalid User");
                }
                var jobs = await _jobService.GetJobsWithKeyword(keyword, skip, pageSize);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostJob")]
            [Authorize]
            public async Task<IActionResult> PostJob([FromBody] SaveJobResources savejobResources)
            {
                try
                {

                    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                    if (userIdClaim == null)
                    {
                        return Unauthorized("Unauthorized");
                    }

                    string userId = userIdClaim.Value;
                    var user = await _userService.GetUserByIdWithTags(int.Parse(userId));
                    if (user == null)
                    {
                        return BadRequest("Invalid User");
                    }
                var newJob = _mapper.Map<SaveJobResources, Job>(savejobResources);
                newJob.Tags.Clear();
                if (savejobResources.Tags.Count <= 5)
                {
                    foreach (var tag in savejobResources.Tags)
                    {
                        var newTag = await _tagService.GetTagById(tag.TagId);
                        if (newTag == null)
                        {
                            return BadRequest($"Tag with ID {tag} not found");
                        }
                        newJob.Tags.Add(newTag);
                        
                    }
                }
                else
                {
                    return BadRequest("Exceeded allowed tags");
                }
                newJob.UserId = int.Parse(userId);
                Job createdJob = await _jobService.CreateJob(newJob);
                var returnnewjobResources = _mapper.Map<Job, JobResources>(createdJob);

                return Ok(returnnewjobResources);
                }
                catch (Exception ex)
                {
                _logger.LogError(ex, "An error occurred while saving the entity changes.");

                // Check if there is an inner exception
                if (ex.InnerException != null)
                {
                    // Log the inner exception for more details
                    _logger.LogError(ex.InnerException, "Inner exception details:");
                }

                return BadRequest(ex.Message);
                }
            }
        [HttpPut("UpdateJob")]
        [Authorize]
        public async Task<IActionResult> UpdateJob([FromBody] UpdateJobResource jobResources)
        {
            try
            {

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized");
                }

                string userId = userIdClaim.Value;
                var user = await _userService.GetUserByIdWithTags(int.Parse(userId));
                if (user == null)
                {
                    return BadRequest("Invalid User");
                }
                var newJob = _mapper.Map<UpdateJobResource, Job>(jobResources);
                Job oldJob = await _jobService.GetJobById(newJob.JobId);
                oldJob.Title = newJob.Title;
                oldJob.Description = newJob.Description;
                oldJob.Offer = newJob.Offer;
                if (oldJob.UserId.ToString() != userId)
                {
                    return BadRequest("Unauthorized User");
                }
                oldJob.Tags.Clear();
                if (jobResources.Tags.Count <= 5)
                {
                    foreach (var tag in jobResources.Tags)
                    {
                        var newTag = await _tagService.GetTagById(tag.TagId);
                        if (newTag == null)
                        {
                            return BadRequest($"Tag with ID {tag} not found");
                        }
                        oldJob.Tags.Add(newTag);

                    }
                }
                else
                {
                    return BadRequest("Exceeded allowed tags");
                }
                //newJob.UserId = int.Parse(userId);
                
                var returnnewjobResources = _mapper.Map<Job, JobResources>(oldJob);
                await _jobService.CommitChanges();
                return Ok(returnnewjobResources);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the entity changes.");

                // Check if there is an inner exception
                if (ex.InnerException != null)
                {
                    // Log the inner exception for more details
                    _logger.LogError(ex.InnerException, "Inner exception details:");
                }

                return BadRequest(ex.Message);
            }
        }
    }
}

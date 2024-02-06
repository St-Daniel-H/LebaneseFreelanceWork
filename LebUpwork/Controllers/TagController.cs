using AutoMapper;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Resources;
using LebUpwork.Api.Resources.Save;
using LebUpwork.Api.Resources.Update;
using LebUpwork.Api.Settings;
using LebUpwork.Api.Validators;
using LebUpwork.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LebUpwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        public TagController(ITagService tagService, IMapper mapper)
        {
            this._mapper = mapper;
            this._tagService = tagService;
        }
        [HttpGet("GetTags")]
        [Authorize]
        public async Task<IActionResult> GetAllTags()
        {
            try
            {
                var tags = await _tagService.GetAll();
                var Tagsresources= _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResources>>(tags);
                return Ok(Tagsresources);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateTag")]
        [Authorize]
        public async Task<IActionResult> CreateTag([FromForm] SaveTagResources resources)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // User ID from the JWT
                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized");
                }

                string userId = userIdClaim.Value;

                // Check if the tag name is unique
                var existingTag = await _tagService.GetTagsByName(resources.TagName);
                if (existingTag != null)
                {
                    return Conflict("Tag name must be unique.");
                }

                var tagResource = _mapper.Map<SaveTagResources, Tag>(resources);

                // Set the user ID for the new tag
                tagResource.AddedByUserId = userId;

                // Create the new tag
                var createdTag = await _tagService.CreateTag(tagResource);

                // Map the created tag to resources
                var tagResources = _mapper.Map<Tag, TagResources>(createdTag);

                return Ok(tagResources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}

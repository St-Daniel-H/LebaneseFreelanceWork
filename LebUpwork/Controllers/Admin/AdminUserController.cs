using AutoMapper;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Resources;
using LebUpwork.Api.Settings;
using LebUpwork.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LebUpwork.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public AdminUserController(IUserService userService, IMapper mapper)
        {
            this._mapper = mapper;
            this._userService = userService;
        }
        [HttpGet("GetAllUsers")]
        [Authorize(Policy = "AdminPolicy")]//need to edit this laters
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResources>>(users);
            return Ok(userResources);
        }
    }
}

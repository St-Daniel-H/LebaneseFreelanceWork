using AutoMapper;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Resources;
using Microsoft.AspNetCore.Mvc;

namespace LebUpwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      
 
            private readonly IMapper _mapper;
            private readonly IUserService _userService;
            public UserController(IUserService userService, IMapper mapper)
            {
                this._mapper = mapper;
                this._userService = userService;
            }
            [HttpGet("")]
            public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
            {
                var users = await _userService.GetAllUsers();
                var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResources>>(users);
                return Ok(userResources);
            }
        
    }
}

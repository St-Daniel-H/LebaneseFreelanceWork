using AutoMapper;
using LebUpwor.core.Models;
using LebUpwork.Api.Interfaces;
using LebUpwork.Api.Repository;
using LebUpwork.Api.Resources;
using LebUpwork.Api.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace LebUpwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {


        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public UserController(IUserService userService, IMapper mapper, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            this._mapper = mapper;

            this._userService = userService;
            _jwtSettings = jwtSettings.Value;
        }
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResources>>(users);
            return Ok(userResources);
        }
        [HttpPost("Signup")]
        [AllowAnonymous] // Allow unauthenticated access
        public async Task<IActionResult> Signup([FromBody] UserSignupResources signupResources)
        {
            var user = _mapper.Map<UserSignupResources, User>(signupResources);
            // Validate the signup data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           if(await _userService.GetUserByEmail(user.Email) != null)
            {
                return Conflict("email already exists");
            }
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            var newUser = new User {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = hashed,
            };

           
        }
    }
}

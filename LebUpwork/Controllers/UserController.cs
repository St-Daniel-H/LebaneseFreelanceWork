﻿using AutoMapper;
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
using LebUpwork.Api.Validators;
using FluentValidation;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LebUpwork.Api.Resources.Update;
using LebUpwork.Api.Validators.Update;

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
        private readonly FileValidation _fileValidation;

        public UserController(IUserService userService, IMapper mapper, IOptionsSnapshot<JwtSettings> jwtSettings,FileValidation fileValidation)
        {
            this._mapper = mapper;
            this._fileValidation = fileValidation;
            this._userService = userService;
            this._jwtSettings = jwtSettings.Value;
        }
        [HttpGet("UserInfo/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo(string userId)
        {
            try
            {
                User user = await _userService.GetUserById(int.Parse(userId));
                var userResources = _mapper.Map<User,UserResources>(user);
                if (user==null)
                {
                    return BadRequest("User was not found");
                }
                return Ok(userResources);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Signup")]
        [AllowAnonymous] // Allow unauthenticated access
        public async Task<IActionResult> Signup([FromForm] UserSignupResources signupResources)
        {
            try
            {
                var user = _mapper.Map<UserSignupResources, User>(signupResources);
                //validate with validator
                var validator = new UserSignupValidator();
                var validationResult = await validator.ValidateAsync(signupResources);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);
                // Validate the signup data
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _userService.GetUserByEmail(user.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return BadRequest(ModelState);
                }

                    // Generate a 128-bit salt sing a sequence of
                string salt;
                string hashedPassword = _userService.HashPassword(user.Password, out salt);
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = hashedPassword,
                    Salt = salt,
                    RoleId = 1,
                    //optional
                    //PhoneNumber = user.PhoneNumber ?? null,
                    //CVpdf =  null,
                    //ProfilePicture = user.ProfilePicture ?? null,

                };
                var createResult = await _userService.CreateUser(newUser);
                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous] // Allow unauthenticated access
        public async Task<IActionResult> Login([FromBody] UserLoginResources loginResources)
        {
            try
            {
                var user = _mapper.Map<UserLoginResources, User>(loginResources);
                // Validate the login data
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                User getUser = await _userService.GetUserByEmail(user.Email);
                if (getUser == null)
                {
                    ModelState.AddModelError("Email", "Email does not exists");
                    return BadRequest(ModelState);
                }

                if (!_userService.CheckPassword(getUser, user.Password))
                {
                    ModelState.AddModelError("Password", "incorrect password");
                    return BadRequest(ModelState);
                }
                // Return the token as a response
                return Ok(GenerateJwt(getUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //update profile picture
        [Authorize]
        [HttpPut("UpdatePfp")]
        public async Task<IActionResult> UpdateProfilePicture([FromForm] UpdateUserProfilePicture ProfilePictureResources)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);//userid from the jwt
                if (userIdClaim == null)
                {
                    // Handle case where user ID claim is missing
                    return Unauthorized("Unauthorized");
                }
                string userId = userIdClaim.Value;//the value of the userid from the jwt
                
                var userResource = _mapper.Map<UpdateUserProfilePicture, User>(ProfilePictureResources);
                User user =await _userService.GetUserById(int.Parse(userId));
                if(user == null)
                {
                    return BadRequest("User was not found");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var uploadsFolderPath = "../LebUpWork/Uploads/ProfilePicture/";
                if (ProfilePictureResources.ProfilePicture != null && ProfilePictureResources.ProfilePicture.Length > 0)
                {
                    if (!_fileValidation.IsImageValid(ProfilePictureResources.ProfilePicture))
                    {
                        return BadRequest("Invalid Image format, please choose jpg,png or gif image");
                    }
                    // Generate the filename using the CompanyId or any other unique identifier
                    var guidFileName = user.UserId + Path.GetExtension(ProfilePictureResources.ProfilePicture.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, guidFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath); // Delete the old file
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePictureResources.ProfilePicture.CopyToAsync(stream);
                    }

                    // Set the file path in the companyToCreate object to be "Uploads/companyId.jpg"
                    user.ProfilePicture = guidFileName;
                    await _userService.CommitChanges();
                    return Ok("Image updated");
                }
                else
                {
                    return BadRequest("Invalid Image");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdateCV")]
        public async Task<IActionResult> UpdateCV([FromForm] UpdateUserCV CVResources)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);//userid from the jwt
                if (userIdClaim == null)
                {
                    // Handle case where user ID claim is missing
                    return Unauthorized("Unauthorized");
                }
                string userId = userIdClaim.Value;//the value of the userid from the jwt

                var userResource = _mapper.Map<UpdateUserCV, User>(CVResources);
                User user = await _userService.GetUserById(int.Parse(userId));
                if (user == null)
                {
                    return BadRequest("User was not found");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var uploadsFolderPath = "../LebUpWork/Uploads/CV/";
                if (CVResources.CVpdf != null && CVResources.CVpdf.Length > 0)
                {
                    if (!_fileValidation.IsFileValid(CVResources.CVpdf))
                    {
                        return BadRequest("Invalid File format, please choose pdf File");
                    }
                    // Generate the filename using the CompanyId or any other unique identifier
                    var guidFileName = user.UserId + Path.GetExtension(CVResources.CVpdf.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, guidFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath); // Delete the old file
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CVResources.CVpdf.CopyToAsync(stream);
                    }

                    // Set the file path in the companyToCreate object to be "Uploads/companyId.jpg"
                    user.ProfilePicture = guidFileName;
                    await _userService.CommitChanges();
                    return Ok("CV updated");
                }
                else
                {
                    return BadRequest("Invalid CV");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateUserStatusResources StatusResources)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);//userid from the jwt
                if (userIdClaim == null)
                {
                    // Handle case where user ID claim is missing
                    return Unauthorized("Unauthorized");
                }
                string userId = userIdClaim.Value;//the value of the userid from the jwt

                var userResource = _mapper.Map<UpdateUserStatusResources, User>(StatusResources);
                User user = await _userService.GetUserById(int.Parse(userId));
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                //validate with validator
                var validator = new UpdateStatusValidator();
                var validationResult = await validator.ValidateAsync(StatusResources);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user.Status=StatusResources.Status;
                await _userService.CommitChanges();
                return Ok("Status Updated");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //generateJWt
        private string GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
             //  new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
               new Claim(ClaimTypes.Role, user.Role.RoleName),  
               new Claim(ClaimTypes.Name,user.UserId.ToString()),
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

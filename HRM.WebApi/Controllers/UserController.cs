using HRM.BL.Interface;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UserController> _logger;
        private IConfiguration _configuration;
        public UserController(IUserManager userManager,ILogger<UserController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }
        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            try 
            {
                Response<UserDto> newUser = await _userManager.Create(createUserDto);
                if(newUser.ErrorCode==0) return Ok(newUser);
                else return BadRequest(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Create ",ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected,"Un expected error in create - userControler"));
            }
        }
        [HttpGet]
        [Authorize(Roles = "manager")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Response<UserDto> user = _userManager.GetByID(id);
                if (user.ErrorCode == 0) return Ok(user);
                else return BadRequest(user);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Get ", ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in get - userControler"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public IActionResult GetAll(UserPaging pagging)
        {
            try
            {
                Response<List<UserDto>> users = _userManager.GetUsersList(pagging);
                if (users.ErrorCode == 0) return Ok(users);
                else return BadRequest(users);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - GetAll",ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in getAll - userControler"));
            }
        }
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                Response<bool> isDeleted = await _userManager.Delete(id);
                if (isDeleted.ErrorCode == 0) return Ok(isDeleted);
                else return BadRequest(isDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Remove", ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in remove - userControler"));
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                var user = Authenticate(loginDto);
                if (user!=null)
                {
                    var token = Generate(user); 
                    return Ok(token);
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Login", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Un expected error in login - userControler"));
            }
        }

        private string Generate(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userDto.Name),
                new Claim(ClaimTypes.Role, userDto.Type.ToString()),
                new Claim(ClaimTypes.MobilePhone, userDto.MobileNumber),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.UserData, userDto.JobTitle),
                new Claim(ClaimTypes.Gender,userDto.Manager.ID.ToString())
            };


        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto Authenticate(LoginDto loginDto)
        {
            var islogin = _userManager.Login(loginDto);
            if (islogin.ErrorCode == 0)
            {
                var user = _userManager.GetByID(loginDto.UserName);
                return user.Data;
            }
            return null;
          
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {

                Response<bool> resetPass = await _userManager.ResetPassword(resetPasswordDto);
                if (resetPass.ErrorCode == 0) return Ok(resetPass);
                else return BadRequest(resetPass);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - ResetPassword", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Un expected error in ResetPassword - userControler"));
            }
        }
        [HttpPut]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Update(UserDto user)
        {
            try
            {
                Response<UserDto> updatedUser =await _userManager.Update(user);
                if (updatedUser.ErrorCode == 0) return Ok(updatedUser);
                else return BadRequest(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Update", ex);
                return BadRequest(new Response<UserDto>(ErrorCodes.Unexpected, "Un expected error in update - userControler"));
            }
        }
        private UserDto GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDto
                {
                    ID = Guid.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value),
                    Type =(UserType) int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value)
                };
            }
         
            return null;
        }

    }
}

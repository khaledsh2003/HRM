using HRM.BL.Interface;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserController(IUserManager userManager,ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            try
            {
                Response<UserDto> newUser = await _userManager.Create(user);
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

        [HttpGet]
        public IActionResult GetAll(Guid managerID, Paging page)
        {
            try
            {
                Response<List<UserDto>> users = _userManager.GetUsersList(managerID,page);
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
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                Response<bool> islogin = _userManager.Login(loginDto);
                if (islogin.ErrorCode == 0) return Ok(islogin);
                else return BadRequest(islogin);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("UserController - Login", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Un expected error in login - userControler"));
            }
        }
        [HttpPut]
        public IActionResult ResetPassword(LoginDto loginDto)
        {
            try
            {
                Response<bool> resetPass = _userManager.ResetPassword(loginDto);
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
    }
}

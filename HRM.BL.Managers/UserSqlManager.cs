using HRM.BL.Interface;
using HRM.DAL.EF;
using HRM.Mapping;
using HRM.Models;
using HRM.PasswordHashing;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace HRM.BL.Managers
{
    public class UserSqlManager : IUserManager 
    {
        private readonly IUserManager _userManager;
        private readonly HrmContext _hrmContext;
        private UserEntityMapper _userEntityMapper;
        private PasswordHash _hashPassword;
        public string _password { get; set; }
        public UserSqlManager(IUserManager userManager, HrmContext hrmContext)
        {
            _userManager = userManager;
            _hrmContext = hrmContext;
            _userEntityMapper = new UserEntityMapper();
        }
        public Response<UserDto> Create(UserDto user)
        {
            try
            {
                string hashedPassword = PasswordHash.HashText(_password,"khaled", new SHA1CryptoServiceProvider());
                var userToCreate = new UserEntity() { Name = user.Name, Type = user.Type, MobileNumber = user.MobileNumber, Email = user.Email, Password = hashedPassword, JobTitle = user.JobTitle, ManagerID = user.ManagerID, CreationDate = DateTime.Now };
                _hrmContext.Users.Add(userToCreate);
                _hrmContext.SaveChanges();
                return new Response<UserDto>(_userEntityMapper.Map(userToCreate));
            }
            catch (Exception ex)
            {
                return new Response<UserDto>(ErrorCodes.Unexpected,"Unexpected Error");
            }
        }
        public Response<UserDto> GetByID(Guid id)
        {
            try
            {
                var user = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
                if(user==null) return new Response<UserDto>(ErrorCodes.UserNotFound, "No user found with such ID");
                return new Response<UserDto>(_userEntityMapper.Map(user));
            }
            catch (Exception ex)
            { 
                return new Response<UserDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        
        public Response<List<UserDto>> GetUsersList([FromQuery] Paging @param)
        {

            List<UserDto> _users = new List<UserDto>();
\            try
            {
                var choosenUser = _hrmContext.Users.Skip((@param.Page - 1)* @param.ItemsPerPage).Take(@param.ItemsPerPage).Where(x=>x.ManagerID==@param.ManagerID).ToList();
                foreach (var i in choosenUser)
                {
                    _users.Add(_userEntityMapper.Map(i));
                }
                return new Response<List<UserDto>>(_users);
            }
            catch (Exception ex)
            {
                return new Response<List<UserDto>>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public Response<UserDto> Update(UserDto user)
        {
            try
            {
                if (!IsUserAval(user.ID))
                {
                    return new Response<UserDto>(ErrorCodes.UserNotFound, "User Not Found ");
                }
                var userToUpdate = _hrmContext.Users.FirstOrDefault(x => x.ID == user.ID);
                if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                if (!string.IsNullOrEmpty(user.MobileNumber)) userToUpdate.MobileNumber = user.MobileNumber;
                if (!string.IsNullOrEmpty(user.Email)) userToUpdate.Email = user.Email;
                if (!string.IsNullOrEmpty(user.JobTitle)) userToUpdate.JobTitle = user.JobTitle;
                if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                _hrmContext.SaveChanges();
                return new Response<UserDto>(_userEntityMapper.Map(userToUpdate));
            }
            catch (Exception ex)
            {
                return new Response<UserDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public Response<bool> Delete(Guid id)
        {
            try
            {
                var userToDelete = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
                if (userToDelete != null)
                {
                    _hrmContext.Remove(userToDelete);
                    _hrmContext.SaveChanges();
                    return new Response<bool>(true);
                }
                return new Response<bool>(false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        private bool IsUserAval(Guid id)
        {
            var user = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
            if (user == null) return false;
            return true;
        }
        public Response<bool> ResetPassword(LoginDto loginDto)
        {
            try
            {
                if (IsUserAval(loginDto.UserName))
                {
                    string hashedPassword = PasswordHash.HashText(loginDto.Password, "khaled", new SHA1CryptoServiceProvider());
                    var user = _hrmContext.Users.FirstOrDefault(x => x.ID == loginDto.UserName);
                    user.Password=hashedPassword;
                    return new Response<bool>(true);
                }
                return new Response<bool>(ErrorCodes.UserNotFound, "User is not found in our record");
            }
            catch (Exception ex)
            {
                return new Response<bool>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public Response<bool> Login(LoginDto loginDto)
        {
            try
            {
                string hashedPassword = PasswordHash.HashText(loginDto.Password, "khaled", new SHA1CryptoServiceProvider());
                var user = _hrmContext.Users.FirstOrDefault(x => x.ID == loginDto.UserName);
                if (user!=null && user.Password==hashedPassword) return new Response<bool>(true);
                return new Response<bool>(ErrorCodes.UserNotFound,"User is not found in our record");
            }
            catch(Exception ex)
            {
                return new Response<bool>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }     
    }
}
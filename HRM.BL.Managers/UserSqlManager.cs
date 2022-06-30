using HRM.BL.Interface;
using HRM.DAL.EF;
using HRM.Mapping;
using HRM.Models;
using HRM.Responses;

namespace HRM.BL.Managers
{
    public class UserSqlManager:IUserManager
    {
        private readonly IUserManager _userManager;
        private readonly HrmContext _hrmContext;
        private UserEntityMapper _userEntityMapper;

        public UserSqlManager(IUserManager userManager,HrmContext hrmContext)
        {
            _userManager = userManager; 
            _hrmContext = hrmContext;
            _userEntityMapper = new UserEntityMapper();
        }
        public UserDto Create(UserDto user)
        {
            try
            {
                var userToCreate = new UserEntity() {Name=user.Name, Type=user.Type,MobileNumber=user.MobileNumber,Email=user.Email,Password=user.Password,JobTitle=user.JobTitle,ManagerID=user.ManagerID,CreationDate=DateTime.Now};
                _hrmContext.Users.Add(userToCreate);
                _hrmContext.SaveChanges();
                return _userEntityMapper.Map(userToCreate);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public UserDto GetByID(Guid id)
        {
            try
            {
                var user = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
                return _userEntityMapper.Map(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Response<List<UserDto>> GetUsersList()
        {
            List<UserDto> _users = new List<UserDto>();
            Response<List<UserDto>> response = new Response<List<UserDto>>();
            try
            {
                var choosenUser = _hrmContext.Users.ToList();
                foreach (var i in choosenUser)
                {
                    _users.Add(_userEntityMapper.Map(i));
                }
                //setting respone data
                response.Data = _users;
                return response;
            }
            catch (Exception ex)
            {
                //setting error code and description if there is an error
                response.ErrorCode= ex.HResult;
                response.Description = ex.Message;
                return response;
            }
        }
        public UserDto Update(UserDto user)
        {
            try
            {
                if (IsUserAval(user.ID))
                {
                    var userToUpdate = _hrmContext.Users.FirstOrDefault(x => x.ID==user.ID);
                    if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                    if (!string.IsNullOrEmpty(user.MobileNumber)) userToUpdate.MobileNumber = user.MobileNumber;
                    if (!string.IsNullOrEmpty(user.Email)) userToUpdate.Email = user.Email;
                    if (!string.IsNullOrEmpty(user.Password)) userToUpdate.Password = user.Password;
                    if (!string.IsNullOrEmpty(user.JobTitle)) userToUpdate.JobTitle = user.JobTitle;
                    if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                    _hrmContext.SaveChanges();
                    return _userEntityMapper.Map(userToUpdate);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Delete(Guid id)
        {
            try
            {
                var userToDelete = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
                if (userToDelete != null)
                {
                    _hrmContext.Remove(userToDelete);
                    _hrmContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsUserAval(Guid id)
        {
            var user = _hrmContext.Users.FirstOrDefault(x => x.ID == id);
            if(user == null) return false;
            return true;
        }
        //do encryption function
    }
}
using HRM.Models;
using HRM.Responses;

namespace HRM.BL.Interface
{
    public interface IUserManager
    {
        public Response<UserDto> Create(UserDto user);
        public Response<UserDto> GetByID(Guid id);
        public Response<List<UserDto>> GetUsersList();
        public Response<UserDto> Update(UserDto user);
        public Response<bool> Delete(Guid id);
        public Response<bool> Login(LoginDto loginDto);
        public Response<bool> ResetPassword(LoginDto loginDto);

    }
}
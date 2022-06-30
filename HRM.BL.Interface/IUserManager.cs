using HRM.Models;
using HRM.Responses;

namespace HRM.BL.Interface
{
    public interface IUserManager
    {
        public UserDto Create(UserDto user);
        public UserDto GetByID(Guid id);
        public Response<List<UserDto>> GetUsersList();
        public UserDto Update(UserDto user);
        public bool Delete(Guid id);
    }
}
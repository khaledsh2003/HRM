using HRM.Models;

namespace HRM.BL.Interface
{
    public interface IUserManager
    {
        public UserDto Create(UserDto user);
        public UserDto GetByID(int id);
        public List<UserDto> GetUsersList();
        public UserDto Update(UserDto user);
        public bool Delete(int id);
    }
}
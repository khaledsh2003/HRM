using HRM.Models;

namespace HRM.BL.Interface
{
    public interface IUserManager
    {
        public UserDto Create(UserDto user);
        public UserDto GetByID(Guid id);
        public List<UserDto> GetUsersList();
        public UserDto Update(UserDto user);
        public bool Delete(Guid id);
    }
}
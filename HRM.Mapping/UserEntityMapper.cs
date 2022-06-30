using HRM.DAL.EF;
using HRM.Models;

namespace HRM.Mapping
{
    public class UserEntityMapper
    {
        public UserDto Map(UserEntity user)
        {
            var temp = new UserDto();
            temp.ID = user.ID;
            temp.Name = user.Name;
            temp.Type = user.Type;
            temp.MobileNumber = user.MobileNumber;
            temp.Email = user.Email;
            temp.Password = user.Password;
            temp.JobTitle = user.JobTitle;
            temp.ManagerID = user.ManagerID;
            return temp;
        }
    }
}
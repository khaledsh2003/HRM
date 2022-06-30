using HRM.DAL.EF;
using HRM.Models;

namespace HRM.Mapping
{
    public class UserEntityMapper
    {
        public UserDto Map(UserEntity user)
        {
            var temp = new UserDto
            {
                ID = user.ID,
                Name = user.Name,
                Type = user.Type,
                MobileNumber = user.MobileNumber,
                Email = user.Email,
                Password = user.Password,
                JobTitle = user.JobTitle,
                ManagerID = user.ManagerID
            };
            return temp;
        }
    }
}
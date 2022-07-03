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
                JobTitle = user.JobTitle,
                Manager = new UserDto()
            };
            if (user.ManagerID != null || user.ManagerID != Guid.Empty)
            {
                temp.Manager = new UserDto();
                temp.Manager.ID = user.ID;
                temp.Manager.Name = user.Name;
                temp.Manager.MobileNumber = user.MobileNumber;
                temp.Manager.Email = user.Email;
                temp.Manager.JobTitle = user.JobTitle;    
            }
            return temp;
        }
    }
}
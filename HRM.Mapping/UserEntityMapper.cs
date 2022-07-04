using HRM.DAL.EF;
using HRM.Models;

namespace HRM.Mapping
{
    public class UserEntityMapper
    {
        public UserDto Map(UserEntity user,UserEntity manager)
        {
            var temp = new UserDto
            {
                ID = user.ID,
                Name = user.Name,
                Type = user.Type,
                MobileNumber = user.MobileNumber,
                Email = user.Email,
                JobTitle = user.JobTitle,
                Manager = new ManagerDto()
            };
            if (user.ManagerID != null || user.ManagerID != Guid.Empty)
            {
                temp.Manager = new ManagerDto();
                temp.Manager.ID = manager.ID;
                temp.Manager.Name = manager.Name;
                temp.Manager.Type = manager.Type;
                temp.Manager.MobileNumber = manager.MobileNumber;
                temp.Manager.Email = manager.Email;
                temp.Manager.JobTitle = manager.JobTitle;    
            }
            return temp;
        }
    }
}
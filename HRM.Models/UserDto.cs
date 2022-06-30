namespace HRM.Models
{
    public class UserDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public Guid ManagerID { get; set; }
        public DateTime CreationDate { get; set; }
        public UserDto()
        {

        }
        public UserDto(Guid id, string name, string mobileNum, string email,string password,string jobtitle,Guid managerId)
        {
            ID = id;
            Name = name;
            MobileNumber = mobileNum;
            Email = email;
            Password = password;
            JobTitle = jobtitle;
            ManagerID = managerId;
        }
    }
}




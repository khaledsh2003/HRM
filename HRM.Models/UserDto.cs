namespace HRM.Models
{
    public class UserDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool Type { get; set; }

        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public Guid? ManagerID { get; set; }
    }
   
}




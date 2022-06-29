using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DAL
{
    public class UserEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UserID {get;set;} 
        public string Name { get;set;}
        public string MobileNumber { get;set;}
        public string Email { get;set;}
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public Guid? ManagerID { get; set; }

        
    }
}

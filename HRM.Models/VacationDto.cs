using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class VacationDto
    {
        public Guid ID { get; set; }
        public int Type { get; set; }
        public DateTime StartingDate { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public Guid UserId { get; set; }
    }
   
}

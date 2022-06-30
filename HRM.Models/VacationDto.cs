using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class VacationDto
    {
        public Guid ID { get;}
        public int Type { get; }
        public DateTime StartingDate { get;}
        public int Duration { get;}
        public int Status { get;}
        public string Note { get;}
        public Guid UserId { get; }
    }
}

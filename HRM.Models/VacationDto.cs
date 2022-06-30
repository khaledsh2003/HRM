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
        public VacationDto()
        {

        }
        public VacationDto(Guid id,int type,DateTime startingDate,int duration,int status,string note,Guid userId)
        {
            ID= id;
            Type= type;
            StartingDate= startingDate;
            Duration= duration;
            Status= status;
            Note= note;
            UserId= userId;
        }
    }
}

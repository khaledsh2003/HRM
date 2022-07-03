using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.BL.Interface
{
    public interface IVacationManager
    {
        public VacationDto Create(VacationDto vacation);
        public List<VacationDto> GetVacationList();
        public VacationDto Update(VacationDto vacation);
        public bool Delete(Guid id);
    }
}

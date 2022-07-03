using HRM.Models;
using HRM.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.BL.Interface
{
    public interface IVacationManager
    {
        public Response<VacationDto> Create(VacationDto vacation);
        public Response<List<VacationDto>> GetVacationList();
        public Response<VacationDto> Update(VacationDto vacation);
        public Response<bool> Delete(Guid id);
    }
}

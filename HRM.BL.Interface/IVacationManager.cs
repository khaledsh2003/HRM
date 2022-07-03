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
        public Task<Response<VacationDto>> Create(VacationDto vacation);
        public Task<Response<List<VacationDto>>> GetVacationList();
        public Task<Response<VacationDto>> Update(VacationDto vacation);
        public Task<Response<bool>> Delete(Guid id);
    }
}

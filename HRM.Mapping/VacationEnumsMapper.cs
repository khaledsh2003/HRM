using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Mapping
{
    public class VacationEnumsMapper
    {
       
        public static VacationDto Map(VacationDto vacationDto)
        {
            
            switch (vacationDto.Type)
            {
                case VacationTypes.Annual:
                    vacationDto.Type = VacationTypes.Annual;
                    break;
                case VacationTypes.Sick:
                    vacationDto.Type = VacationTypes.Sick;
                    break;
                case VacationTypes.Leave:
                    vacationDto.Type = VacationTypes.Leave;
                    break;
                case VacationTypes.Exceptional:
                    vacationDto.Type = VacationTypes.Exceptional;
                    break;
                default:
                    break;
            }
            return vacationDto;

        }
    }
}

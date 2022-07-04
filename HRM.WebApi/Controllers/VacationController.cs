using HRM.BL.Interface;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationManager _vacationManager;
        private readonly ILogger<VacationController> _logger;
        public VacationController(IVacationManager vacationManager, ILogger<VacationController> logger)
        {   
            _vacationManager = vacationManager;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VacationDto vacation)
        {
            try
            {
                Response<VacationDto> newVacation = await _vacationManager.Create(vacation);
                if (newVacation.ErrorCode == 0) return Ok(newVacation); 
                else return BadRequest(newVacation);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("VacationController - Create", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Unexpected error in VacationController - Create"));
            }
        }
        [HttpGet]
        public IActionResult GetAll(Paging page)
        {
            try
            {
                Response<List<VacationDto>> vacation = _vacationManager.GetVacationList(page);
                if (vacation.ErrorCode == 0) return Ok(vacation);
                else return BadRequest(vacation);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("VacationController - GetAll", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Unexpected error in VacationController - GetAll"));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                Response<bool> isDeleted = await _vacationManager.Delete(id);
                if (isDeleted.ErrorCode == 0) return Ok(isDeleted);
                else return BadRequest(isDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("VacationController - Remove", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Unexpected error in VacationController - Remove"));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(VacationDto vacationDto)
        {
            try
            {
                Response<VacationDto> updatedVacation = await _vacationManager.Update(vacationDto);
                if (updatedVacation.ErrorCode == 0) return Ok(updatedVacation);
                else return BadRequest(updatedVacation);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("VacationController - Update", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Unexpected error in VacationController - Update"));
            }
        }
    }
}

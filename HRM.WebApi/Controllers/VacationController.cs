﻿using HRM.BL.Interface;
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
                Task<Response<VacationDto>> newVacation = _vacationManager.Create(vacation);
                if (newVacation.Result.ErrorCode == 0) return Ok(newVacation.Result); 
                else return BadRequest(newVacation.Result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("VacationController - Create", ex);
                return BadRequest(new Response<bool>(ErrorCodes.Unexpected, "Unexpected error in VacationController - Create"));
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                Response<List<VacationDto>> vacation = _vacaionManager.GetVacationList();
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
        public IActionResult Remove(Guid id)
        {
            try
            {
                Response<bool> isDeleted = _vacaionManager.Delete(id);
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
        public IActionResult Update(VacationDto vacationDto)
        {
            try
            {
                Response<VacationDto> updatedVacation = _vacaionManager.Update(vacationDto);
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
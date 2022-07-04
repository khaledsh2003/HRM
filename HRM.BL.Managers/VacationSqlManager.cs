﻿using HRM.BL.Interface;
using HRM.DAL.EF;
using HRM.Mapping;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRM.BL.Managers
{
    public class VacationSqlManager : IVacationManager
    {
        private readonly IVacationManager _vacationManager;
        private readonly HrmContext _hrmContext;
        private VacationEntityMapper _vacationEntityMapper;
        VacationSqlManager(IVacationManager vacationManager, HrmContext hrmContext)
        {
            _vacationManager = vacationManager;
            _hrmContext = hrmContext;
            _vacationEntityMapper = new VacationEntityMapper();
        }
        public async Task<Response<VacationDto>> Create(VacationDto vacation)
        {
            try
            {
                var vacationToCreate = new VacationEntity() { Type = vacation.Type, StartingDate = vacation.StartingDate, Duration = vacation.Duration, Status = vacation.Status, Note = vacation.Note, UserId = vacation.UserId, CreationDate = DateTime.Now };
                _hrmContext.Vacations.Add(vacationToCreate);
                await _hrmContext.SaveChangesAsync();
                return new Response<VacationDto>(_vacationEntityMapper.Map(vacationToCreate));
            }
            catch (Exception ex)
            {
                return new Response<VacationDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public Response<List<VacationDto>> GetVacationList([FromQuery] Paging @param)
        {
            List<VacationDto> _vacation = new List<VacationDto>();
            try
            {
                var choosenVacation = _hrmContext.Vacations.Skip((@param.Page - 1) * @param.ItemsPerPage).Take(@param.ItemsPerPage).ToList(); ;
                foreach (var i in choosenVacation)
                {
                    _vacation.Add(_vacationEntityMapper.Map(i));
                }
                return new Response<List<VacationDto>>(_vacation);
            }
            catch (Exception ex)
            {
                return new Response<List<VacationDto>>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public async Task<Response<VacationDto>> Update(VacationDto vacation)
        {
            try
            {
                if (IsVacationAval(vacation.ID))
                {
                    return new Response<VacationDto>(ErrorCodes.UserNotFound, "User Not Found ");
                }
                var vacationToUpdate = _hrmContext.Vacations.FirstOrDefault(x => x.ID == vacation.ID);
                if (vacation.Type > 0 && vacation.Type <= 3) vacationToUpdate.Type = vacation.Type;
                if (!string.IsNullOrEmpty(vacation.StartingDate.ToString())) vacationToUpdate.StartingDate = vacation.StartingDate;
                if (vacation.Duration > 0) vacationToUpdate.Duration = vacation.Duration;
                if (vacation.Status >= 0 && vacation.Status <= 3) vacationToUpdate.Status = vacation.Status;
                if (!string.IsNullOrEmpty(vacation.Note)) vacationToUpdate.Note = vacation.Note;
                await _hrmContext.SaveChangesAsync();
                return new Response<VacationDto>(_vacationEntityMapper.Map(vacationToUpdate));
            }
            catch (Exception ex)
            {
                return new Response<VacationDto>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        public async Task<Response<bool>> Delete(Guid id)
        {
            try
            {
                var vacationToDelete = _hrmContext.Vacations.FirstOrDefault(x => x.ID == id);
                if (vacationToDelete != null)
                {
                    _hrmContext.Remove(vacationToDelete);
                    await _hrmContext.SaveChangesAsync();
                    return new Response<bool>(true);
                }
                return new Response<bool>(false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(ErrorCodes.Unexpected, "Unexpected Error");
            }
        }
        private bool IsVacationAval(Guid id)
        {
            var vacation = _hrmContext.Vacations.FirstOrDefault(x => x.ID == id);
            if (vacation == null) return false;
            return true;
        }
    }
}

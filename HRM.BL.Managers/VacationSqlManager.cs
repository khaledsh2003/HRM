using HRM.BL.Interface;
using HRM.DAL.EF;
using HRM.Mapping;
using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.BL.Managers
{
    public class VacationSqlManager: IVacationManager
    {
        private readonly IVacationManager _vacationManager;
        private readonly HrmContext _hrmContext;
        private VacationEntityMapper _vacationEntityMapper;
        VacationSqlManager(IVacationManager vacationManager,HrmContext hrmContext)
        {
            _vacationManager= vacationManager;
            _hrmContext= hrmContext;
            _vacationEntityMapper = new VacationEntityMapper();
        }
        public VacationDto Create(VacationDto vacation)
        {
            try
            {
                var vacationToCreate = new VacationEntity() {Type=vacation.Type, StartingDate=vacation.StartingDate,Duration=vacation.Duration,Status=vacation.Status,Note=vacation.Note,UserId=vacation.UserId,CreationDate=DateTime.Now};
                _hrmContext.Vacations.Add(vacationToCreate);
                _hrmContext.SaveChanges();
                return _vacationEntityMapper.Map(vacationToCreate);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<VacationDto> GetVacationList()
        {
            List<VacationDto> _vacation = new List<VacationDto>();
            try
            {
                var choosenVacation = _hrmContext.Vacations.ToList();
                foreach (var i in choosenVacation)
                {
                    _vacation.Add(_vacationEntityMapper.Map(i));
                }
                return _vacation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public VacationDto Update(VacationDto vacation)
        {
            try
            {
                if (IsVacationAval(vacation.ID))
                {
                    var userToUpdate = _hrmContext.Users.FirstOrDefault(x => x.ID == user.ID);
                    if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                    if (!string.IsNullOrEmpty(user.MobileNumber)) userToUpdate.MobileNumber = user.MobileNumber;
                    if (!string.IsNullOrEmpty(user.Email)) userToUpdate.Email = user.Email;
                    if (!string.IsNullOrEmpty(user.Password)) userToUpdate.Password = user.Password;
                    if (!string.IsNullOrEmpty(user.JobTitle)) userToUpdate.JobTitle = user.JobTitle;
                    if (!string.IsNullOrEmpty(user.Name)) userToUpdate.Name = user.Name;
                    _hrmContext.SaveChanges();
                    return _userEntityMapper.Map(userToUpdate);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Delete(int id)
        {

        }
        public bool IsVacationAval(Guid id)
        {
            var vacation = _hrmContext.Vacations.FirstOrDefault(x => x.ID == id);
            if (vacation == null) return false;
            return true;
        }
    }
}

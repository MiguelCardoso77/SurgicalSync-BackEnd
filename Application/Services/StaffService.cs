using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDNetCore.Application.Services
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        
        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        } 
        
        public async Task<List<StaffDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<StaffDto> listDto = list.ConvertAll<StaffDto>(staff => new StaffDto
            {
                Id = staff.Id.AsString(),
                StaffName = staff.StaffName.ToString(),
                StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(),
                StaffSpecialization = staff.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                StaffType = staff.StaffType.ToString()
            });

            return listDto;
        }
        
        public async Task<StaffDto> GetByIdAsync(LicenseNumber id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
            {
                return null;
            }

            return null;
        }

        
        
        
    }
}
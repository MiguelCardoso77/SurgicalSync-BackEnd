using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class StaffIdService
    {
        private static int _lastGeneratedNumber = 00003;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly StaffMapper _mapper;

        public StaffIdService(IUnitOfWork unitOfWork, IStaffRepository repo, StaffMapper mapper, StaffService staffService)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
            
        }
        public static StaffId GenerateLN(StaffDto staffDto,StaffType staffType, List<Staff> list)
        {
            bool exists = list.Any(s => s.StaffPhoneNumber.ToString() == staffDto.StaffPhoneNumber || s.UserEmail.ToString() == staffDto.UserEmail);

            if (exists)
            {
                throw new InvalidOperationException("Já existe um registro com o mesmo phone number ou email.");
            }

            
            char prefix = staffType switch
            {
                StaffType.Doctor => 'D',
                StaffType.Nurse => 'N',
                _ => 'O' 
            };
            
            string year = DateTime.Now.Year.ToString();

            _lastGeneratedNumber = (_lastGeneratedNumber + 1) % 10000; 

            string staffId = $"{prefix}{year}{_lastGeneratedNumber:D5}";

            return new StaffId(staffId);
        }
    }
}
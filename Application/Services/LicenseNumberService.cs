using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class LicenseNumberService
    {
        // Variável estática para rastrear o último número gerado
        private static int _lastGeneratedNumber = 00003;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly StaffMapper _mapper;

        public LicenseNumberService(IUnitOfWork unitOfWork, IStaffRepository repo, StaffMapper mapper, StaffService staffService)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
            
        }
        public static LicenseNumber GenerateLN(StaffDto staffDto,StaffType staffType, List<Staff> list)
        {
            bool exists = list.Any(s => s.StaffName.ToString() == staffDto.StaffName && s.StaffEmail.ToString() == staffDto.StaffEmail);

            if (exists)
            {
                throw new InvalidOperationException("Já existe um registro com o mesmo nome e email.");
            }

            // Gera o prefixo com base no tipo de staff
            char prefix = staffType switch
            {
                StaffType.Doctor => 'D',
                StaffType.Nurse => 'N',
                _ => 'O' // Para outros tipos
            };
            
            string year = DateTime.Now.Year.ToString();

            _lastGeneratedNumber = (_lastGeneratedNumber + 1) % 10000; 

            string licenseNumberValue = $"{prefix}{year}{_lastGeneratedNumber:D5}";

            return new LicenseNumber(licenseNumberValue);
        }
    }
}
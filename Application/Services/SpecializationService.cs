using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Specializations;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{

    public class SpecializationService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpecializationRepository _repo;
        private readonly ILogger _logger;
        private readonly SpecializationMapper _mapper;


        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository repo,
            ILogger<SpecializationService> logger, SpecializationMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<List<SpecializationDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.ToListDto(list);
        }


        public async Task<SpecializationDto> GetByIdAsync(SpecializationCode id)
        {
            var specialization = await this._repo.GetByIdAsync(id);

            if (specialization == null)
            {
                return null;
            }

            return _mapper.ToDto(specialization);
        }

        public async Task<List<SpecializationDto>> GetByDesignationAsync(string specializationDesignation)
        {
            var list = await _repo.GetAllAsync();
<<<<<<< Updated upstream
            
            
            var filteredList = list.Where(specialization =>
                    specialization.Designation.ToString().Contains(designation, StringComparison.OrdinalIgnoreCase))
=======

            var filteredList = list.Where(s =>
                    s.Designation.ToString().Equals(specializationDesignation, StringComparison.OrdinalIgnoreCase))
>>>>>>> Stashed changes
                .ToList();
            
            Console.WriteLine("filteredList" + filteredList);

            return _mapper.ToListDto(filteredList);
        }


        public async Task<SpecializationDto> AddAsync(SpecializationDto specializationDto)
        {
<<<<<<< Updated upstream
            if (specializationDto.SpecializationCode.Length > 10 ||
                !System.Text.RegularExpressions.Regex.IsMatch(specializationDto.SpecializationCode,
=======
            if (specializationDto.code.Length > 10 ||
                !System.Text.RegularExpressions.Regex.IsMatch(specializationDto.code,
>>>>>>> Stashed changes
                    @"^[a-zA-Z0-9\-]+$"))
            {
                throw new ArgumentException(
                    "SpecializationCode must be 10 characters or less and contain only letters, numbers, and dashes.");
            }

            var specialization = _mapper.ToDomain(specializationDto);
            await _repo.AddAsync(specialization);
            await _unitOfWork.CommitAsync();
            return _mapper.ToDto(specialization);
        }

        public async Task<SpecializationDto> UpdateAsync(SpecializationDto specializationDto)
        {
            var specialization = await _repo.GetByIdAsync(new SpecializationCode(specializationDto.code));

            if (specialization == null)
                return null;

            var list = await this._repo.GetAllAsync();


            bool exists = list.Any(s =>
                (s.Designation.ToString() == specializationDto.designation && s.Id.ToString() != specializationDto.code)) ;
            
            if (exists)
            {
                throw new InvalidOperationException(
                    "Already exists a specialization with the same code or designation.");
            }
<<<<<<< Updated upstream
            
            specialization.ChangeDesignation(new SpecializationDesignation(specializationDto.SpecializationDesignation));

            specialization.ChangeDescription(new SpecializationDescription(specializationDto.SpecializationDescription));
=======

            specialization.ChangeDesignation(
                new SpecializationDesignation(specializationDto.designation));

            specialization.ChangeDescription(
                new SpecializationDescription(specializationDto.description));
>>>>>>> Stashed changes

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(specialization);
        }
        
<<<<<<< Updated upstream
        public async Task<SpecializationDto> DeleteAsync( SpecializationId id)
        {
            var specialization = await this._repo.GetByIdAsync(id);
            
=======
        public async Task<SpecializationDto> DeleteAsync(SpecializationCode id)
        {
            var specialization = await this._repo.GetByIdAsync(id);
            
            Console.WriteLine(specialization);
>>>>>>> Stashed changes

            if (specialization == null)
                return null;

            this._repo.Remove(specialization);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(specialization);
        }
<<<<<<< Updated upstream
=======
        
>>>>>>> Stashed changes
    }
}
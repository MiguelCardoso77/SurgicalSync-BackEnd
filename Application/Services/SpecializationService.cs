using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        public async Task<SpecializationDto> GetByCodeAsync(string code)
        {
            var specialization = await _repo.GetByIdAsync(new SpecializationId(code));
            return _mapper.ToDto(specialization);
        }

        public async Task<List<SpecializationDto>> GetByDesignationAsync(string designation)
        {
            var list = await _repo.GetAllAsync();

            var filteredList = list.Where(specialization =>
                    specialization.Designation.ToString().Contains(designation, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.ToListDto(filteredList);
        }


        public async Task<SpecializationDto> AddAsync(SpecializationDto specializationDto)
        {
            if (specializationDto.SpecializationCode.Length <= 10 ||
                !System.Text.RegularExpressions.Regex.IsMatch(specializationDto.SpecializationCode,
                    @"^[a-zA-Z0-9\-]+$"))
            {
                throw new ArgumentException(
                    "SpecializationCode must be less than 10 characters long and contain only letters, numbers, and dashes.");
            }

            var specialization = _mapper.ToDomain(specializationDto);
            await _repo.AddAsync(specialization);
            await _unitOfWork.CommitAsync();
            return _mapper.ToDto(specialization);
        }

        public async Task<SpecializationDto> UpdateAsync(SpecializationDto specializationDto)
        {
            var specialization = await _repo.GetByIdAsync(new SpecializationId(specializationDto.SpecializationCode));

            if (specialization == null)
                return null;

            specialization.ChangeDesignation(
                new SpecializationDesignation(specializationDto.SpecializationDesignation));

            specialization.ChangeDescription(
                new SpecializationDescription(specializationDto.SpecializationDescription));

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(specialization);
        }



    }
}
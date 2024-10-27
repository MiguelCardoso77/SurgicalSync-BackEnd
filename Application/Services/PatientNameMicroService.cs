using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class for retrieving operation requests by patient name.
     * This class provides functionality to get all operation requests
     * associated with a specific patient, based on their name.
     */
    public class PatientNameMicroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepository;
        private readonly IOperationRequestRepository _operationRequestRepository;
        private readonly OperationRequestMapper _operationRequestMapper;
        private readonly PatientMapper _patientMapper;
        
        /**
         * Constructor for PatientNameMicroService.
         * Initializes the repositories, unit of work, and mappers.
         *
         * @param unitOfWork The unit of work for transaction management.
         * @param patientRepository Repository for accessing patient data.
         * @param operationRequestRepository Repository for accessing operation requests data.
         */

        public PatientNameMicroService(IUnitOfWork unitOfWork, IPatientRepository patientRepository,
            IOperationRequestRepository operationRequestRepository)
        {
            this._unitOfWork = unitOfWork;
            this._patientRepository = patientRepository;
            this._operationRequestRepository = operationRequestRepository;  
            this._operationRequestMapper = new OperationRequestMapper();
            this._patientMapper = new PatientMapper();
        }
        
        /**
         * Retrieves all operation requests associated with a specific patient name.
         * The method first searches for the patient by their name. If a patient is found,
         * it then retrieves their associated medical record number and filters the operation requests
         * based on this medical record number.
         *
         * @param patientName The name of the patient whose operation requests are being queried.
         * @return A list of operation request DTOs that belong to the patient, or an empty list if no matching patient is found.
         */

        public async Task<List<OperationRequestDto>> GetAllOperationRequestsByPatientName(string patientName)
        {
            var patientList = await _patientRepository.GetAllAsync();
            
            var patient = patientList.FirstOrDefault(p => p.PatientName.ToString().Equals(patientName, StringComparison.OrdinalIgnoreCase));

            if (patient == null)
            {
                return new List<OperationRequestDto>();
            }

            var medicalRecordNumber = patient.Id;
            
            var operationRequests = await _operationRequestRepository.GetAllAsync();
            
            var filteredOperationRequests = operationRequests
                .Where(or => or.MedicalRecordNumber.Equals(medicalRecordNumber))
                .ToList();

            var operationRequestDtos = _operationRequestMapper.ToListDto(filteredOperationRequests);

            return operationRequestDtos;
        }
    }
}
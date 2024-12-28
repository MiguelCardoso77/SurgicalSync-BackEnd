using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Patients;

namespace DDDNetCore.Application.Services;

/**
 * Microservice for managing and retrieving a patient's appointment history.
 */

public class PatientAppointmentHistoryMicroService
{
    private readonly IOperationRequestRepository _operationRequestRepository;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly OperationRequestMapper _operationRequestMapper;
    
    /**
     * Initializes a new instance of the PatientAppointmentHistoryMicroService class.
     *
     * @param operationRequestRepository The repository for operation requests.
     * @param appointmentsRepository The repository for appointments.
     */

    public PatientAppointmentHistoryMicroService(IOperationRequestRepository operationRequestRepository, 
        IAppointmentsRepository appointmentsRepository)
    {
        this._operationRequestRepository = operationRequestRepository;
        this._appointmentsRepository = appointmentsRepository;
        this._operationRequestMapper = new OperationRequestMapper();
    }
    
    /**
    * Retrieves the entire appointment history of a patient based on their medical record number.
    *
    * @param medicalRecordNumber The medical record number of the patient.
    * @return A list of appointment representing the patient's appointment history.
    */

    public async Task<List<Appointment>> GetAllPatientAppointmentHistoryAsync(MedicalRecordNumber medicalRecordNumber)
    {
        var operationRequests = await GetPatientOperationRequests(medicalRecordNumber);

        var appointments = new List<Appointment>();

        foreach (var operationRequestDto in operationRequests)
        {
            var orId = new OperationRequestId(operationRequestDto.OperationRequestId);

            var allAppointments = await _appointmentsRepository.GetAllAsync();

            var relatedAppointments = allAppointments
                .Where(app => app.OperationRequestId.Equals(orId))
                .ToList();

            appointments.AddRange(relatedAppointments);
        }

        return appointments;
    }
    
    /**
    * Retrieves all operation requests associated with a specific medical record number.
    *
    * @param medicalRecordNumber The medical record number of the patient.
    * @return A list of operation request DTOs associated with the patient.
    */

    public async Task<List<OperationRequestDto>> GetPatientOperationRequests(MedicalRecordNumber medicalRecordNumber)
    {
        var allOperationRequests = await _operationRequestRepository.GetAllAsync();
        
        var patientRequests = allOperationRequests.Where(or => or.MedicalRecordNumber.
            Equals(medicalRecordNumber)).ToList();
        
        var patientRequestsDto = _operationRequestMapper.ToListDto(patientRequests);

        return patientRequestsDto;
    }
}
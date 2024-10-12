using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Application.Mappers;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _repo;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<PatientDto> listDto = list.ConvertAll<PatientDto>(patient => new PatientDto
            {
                MedicalRecordNumber = patient.MedicalRecordNumber.AsString(),
                PatientName = patient.PatientName.ToString(),
                BirthDate = patient.BirthDate.ToString(),
                Gender = patient.Gender.ToString(),
                PhoneNumber = patient.PhoneNumber.ToString(),
                MedicalConditions = patient.MedicalConditions.Select(rs => rs.MedicalConditionsValue).ToList(),
                EmergencyContact = patient.EmergencyContact.ToString(),
                AppointmentHistory = patient.AppointmentHistory.Select(rs => rs.AppointmentHistoryValue).ToList()
            });

            return listDto;
        }

        public async Task<PatientDto> GetByIdAsync(MedicalRecordNumber id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            return null;
        }

        public async Task<PatientDto> AddAsync(PatientDto dto)
        {
            var medicalRecordNumber = string.IsNullOrEmpty(dto.MedicalRecordNumber)
                ? new MedicalRecordNumber(Guid.NewGuid().ToString())
                : new MedicalRecordNumber(dto.MedicalRecordNumber);

            var medicalConditionsList = new List<MedicalConditions>();
            var dtoMedicalConditions = dto.MedicalConditions.Select(rs => new MedicalConditions(rs)).ToList();
            medicalConditionsList.AddRange(dtoMedicalConditions);

            var appointmentHistoryList = new List<AppointmentHistory>();
            var dtoAppointmentHistory = dto.AppointmentHistory.Select(rs => new AppointmentHistory(rs)).ToList();
            appointmentHistoryList.AddRange(dtoAppointmentHistory);


            var patient = PatientMapper.ToDomain(dto, medicalRecordNumber, medicalConditionsList, appointmentHistoryList);

            await this._repo.AddAsync(patient);
            await this._unitOfWork.CommitAsync();

            return new PatientDto
            {
                MedicalRecordNumber = patient.MedicalRecordNumber.AsString(),
                PatientName = patient.PatientName.ToString(),
                BirthDate = patient.BirthDate.ToString(),
                Gender = patient.Gender.ToString(),
                PhoneNumber = patient.PhoneNumber.ToString(),
                MedicalConditions = patient.MedicalConditions.Select(rs => rs.MedicalConditionsValue).ToList(),
                EmergencyContact = patient.EmergencyContact.ToString(),
                AppointmentHistory = patient.AppointmentHistory.Select(rs => rs.AppointmentHistoryValue).ToList()
            };
        }

        public async Task<PatientDto> UpdateAsync(PatientDto dto)
        {
            var patient = await this._repo.GetByIdAsync(new MedicalRecordNumber(dto.MedicalRecordNumber));

            if (patient == null)
                return null;

            // change all field
            //patient.ChangeEmergencyContact(dto.EmergencyContact);

            await this._unitOfWork.CommitAsync();

            return new PatientDto
            {
                MedicalRecordNumber = patient.MedicalRecordNumber.AsString(),
                PatientName = patient.PatientName.ToString(),
                BirthDate = patient.BirthDate.ToString(),
                Gender = patient.Gender.ToString(),
                PhoneNumber = patient.PhoneNumber.ToString(),
                MedicalConditions = patient.MedicalConditions.Select(rs => rs.MedicalConditionsValue).ToList(),
                EmergencyContact = patient.EmergencyContact.ToString(),
                AppointmentHistory = patient.AppointmentHistory.Select(rs => rs.AppointmentHistoryValue).ToList()
            };
        }

        public async Task<PatientDto> DeleteAsync(MedicalRecordNumber id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
                return null;

            this._repo.Remove(patient);
            await this._unitOfWork.CommitAsync();

            return new PatientDto
            {
                MedicalRecordNumber = patient.MedicalRecordNumber.AsString(),
                PatientName = patient.PatientName.ToString(),
                BirthDate = patient.BirthDate.ToString(),
                Gender = patient.Gender.ToString(),
                PhoneNumber = patient.PhoneNumber.ToString(),
                MedicalConditions = patient.MedicalConditions.Select(rs => rs.MedicalConditionsValue).ToList(),
                EmergencyContact = patient.EmergencyContact.ToString(),
                AppointmentHistory = patient.AppointmentHistory.Select(rs => rs.AppointmentHistoryValue).ToList()
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Application.Services
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _repo;
        private readonly PatientMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo, PatientMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<PatientDto> listDto = list.ConvertAll<PatientDto>(patient => _mapper.ToDto(patient));

            return listDto;
        }
        
        public async Task<List<PatientDto>> GetAllByPatientName(string patientName)
        {
            var patientsList = await this._repo.GetAllAsync();
            var patient = patientsList.FirstOrDefault(p => p.PatientName.ToString().Equals(patientName, StringComparison.OrdinalIgnoreCase));
            var medicalRecordNumber = patient.Id;
            var filteredPatientsList = patientsList
                .Where(pt => pt.Id.Equals(medicalRecordNumber))
                .ToList();
            
            var patientsListDtos = _mapper.ToListDto(filteredPatientsList);
            
            return patientsListDtos;
        }

        public async Task<List<PatientDto>> GetAllByBirthDate(string birthDate)
        {
            var patientsList = await _repo.GetAllAsync();
    
            var filteredList = patientsList.Where(pt => pt.BirthDate.ToString().Equals(birthDate)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        public async Task<List<PatientDto>> GetAllByMedicalRecordNumber(string medicalRecordNumber)
        {
            var patientsList = await _repo.GetAllAsync();
    
            var filteredList = patientsList.Where(pt => pt.Id.AsString().Equals(medicalRecordNumber)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        public async Task<List<PatientDto>> GetAllByEmail(string email)
        {
            var patientsList = await _repo.GetAllAsync();
    
            var filteredList = patientsList.Where(or => or.UserEmail.ToString().Equals(email)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        public async Task<List<PatientDto>> GetAllByPhoneNumber(string phoneNumber)
        {
            var patientsList = await _repo.GetAllAsync();
    
            var filteredList = patientsList.Where(pt => pt.PhoneNumber.ToString().Equals(phoneNumber)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        public async Task<List<PatientDto>> GetAllByGender(string gender)
        {
            var patientsList = await _repo.GetAllAsync();
    
            var filteredList = patientsList.Where(pt => pt.Gender.ToString().Equals(gender)).ToList();
    
            return _mapper.ToListDto(filteredList);
        }
        
        public async Task<PatientDto> GetByIdAsync(MedicalRecordNumber id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            return _mapper.ToDto(patient);
        }

        public async Task<PatientDto> AddAsync(PatientDto dto)
        {
            var medicalRecordNumber = string.IsNullOrEmpty(dto.MedicalRecordNumber)
                ? new MedicalRecordNumber()
                : new MedicalRecordNumber(dto.MedicalRecordNumber);

            var medicalConditionsList = new List<MedicalConditions>();
            var appointmentHistoryList = new List<AppointmentHistory>();
            
            var patient = _mapper.ToDomain(dto, medicalRecordNumber, medicalConditionsList, appointmentHistoryList);

            await this._repo.AddAsync(patient);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(patient);
        }

        public async Task<PatientDto> UpdateAsync(PatientDto dto)
        {
            var patient = await this._repo.GetByIdAsync(new MedicalRecordNumber(dto.MedicalRecordNumber));

            if (patient == null)
                return null;

            var phoneNumber = patient.PhoneNumber.ToString();
            var emergencyContact = patient.EmergencyContact.ToString();
            var patientEmail = patient.UserEmail.ToString();
            
            // change all fields
            patient.ChangePatientName(new PatientName(dto.PatientName));
            patient.ChangePhoneNumber(new PhoneNumber(dto.PhoneNumber));
            patient.ChangeGender(new Gender(dto.Gender));
            patient.ChangeBirthDate(new BirthDate(dto.BirthDate)); 
            patient.ChangeEmergencyContact(new EmergencyContact(dto.EmergencyContact));
            
            // Send set-up email to user
            var smtpEmailService = new EmailService();
            var emailContent = $"Hello {patient.PatientName}! \n Your  contact information was changed. Now it is : phone number : {phoneNumber}, emergency contact : {emergencyContact} and email : {patientEmail}";
            var email = new Email(emailContent, patient.UserEmail.ToString(), "Changes On Your Contact Information");
            await smtpEmailService.SendEmailAsync(email);
            Console.WriteLine($"Successfully sent the email to {email.Destination}");

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(patient);
        }

        public async Task<PatientDto> DeleteAsync(MedicalRecordNumber id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
                return null;

            this._repo.Remove(patient);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(patient);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class for managing Patient entities.
     * Provides methods to perform CRUD operations and various search/filtering functionalities.
     */
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _repo;
        private readonly PatientMapper _mapper;
        private readonly UserEmailMicroService _userEmailMicroService;
        private readonly DeletePatientMicroService _deletePatientMicroservice;
        private readonly PatientAppointmentHistoryMicroService _patientAppointmentHistoryMicroService;
        private readonly ILogger<PatientService> _logger;

        /**
         * Initializes a new instance of the PatientService class.
         *
         * @param unitOfWork The unit of work implementation used for managing
         *                   transactional operations across multiple repositories.
         * @param repo The patient repository implementation used to access
         *             patient data in the data store.
         * @param mapper The mapper instance used for converting between
         *               domain entities and Data Transfer Objects (DTOs).
         * @param userEmailMicroService The microservice for handling user email
         *                              related operations.
         * @param deletePatientMicroservice The microservice for handling patient
         *                                   deletion operations.
         * @param logger The logger instance for logging information, warnings,
         *               and errors during the execution of the service.
         */
        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo, PatientMapper mapper,
            UserEmailMicroService userEmailMicroService, DeletePatientMicroService deletePatientMicroservice, 
            ILogger<PatientService> logger, PatientAppointmentHistoryMicroService patientAppointmentHistoryMicroService)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
            this._userEmailMicroService = userEmailMicroService;
            this._deletePatientMicroservice = deletePatientMicroservice;
            this._logger = logger;
            this._patientAppointmentHistoryMicroService = patientAppointmentHistoryMicroService;
        }

        /**
         * Retrieves all patients as a list of PatientDto.
         *
         * @return A list of PatientDto representing all patients.
         */
        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<PatientDto> listDto = list.ConvertAll<PatientDto>(patient => _mapper.ToDto(patient));

            return listDto;
        }

        /**
         * Retrieves all patients by the specified patient name.
         *
         * @param patientName The name of the patient to filter by.
         * @return A list of PatientListDto representing patients with the specified name.
         */
        public async Task<List<PatientListDto>> GetAllByPatientNameAsync(string patientName)
        {
            var patientsList = await this._repo.GetAllAsync();

            var filteredPatientsList = patientsList
                .Where(pt => pt.PatientName.ToString().Equals(patientName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.ToListDto(filteredPatientsList);
        }

        /**
         * Retrieves all patients by the specified birth date.
         *
         * @param birthDate The birth date to filter by.
         * @return A list of PatientListDto representing patients with the specified birth date.
         */
        public async Task<List<PatientListDto>> GetAllByBirthDateAsync(string birthDate)
        {
            var patientsList = await _repo.GetAllAsync();

            var filteredList = patientsList.Where(pt => pt.BirthDate.ToString().Equals(birthDate)).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Retrieves all patients by the specified email.
         *
         * @param email The email to filter by.
         * @return A list of PatientListDto representing patients with the specified email.
         */
        public async Task<List<PatientListDto>> GetAllByEmailAsync(string email)
        {
            var patientsList = await _repo.GetAllAsync();

            var filteredList = patientsList.Where(or => or.UserEmail.ToString().Equals(email)).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Retrieves all patients by the specified phone number.
         *
         * @param phoneNumber The phone number to filter by.
         * @return A list of PatientListDto representing patients with the specified phone number.
         */
        public async Task<List<PatientListDto>> GetAllByPhoneNumberAsync(string phoneNumber)
        {
            var patientsList = await _repo.GetAllAsync();

            var filteredList = patientsList.Where(pt => pt.PhoneNumber.ToString().Equals(phoneNumber)).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Retrieves all patients by the specified gender.
         *
         * @param gender The gender to filter by.
         * @return A list of PatientListDto representing patients with the specified gender.
         */
        public async Task<List<PatientListDto>> GetAllByGenderAsync(string gender)
        {
            var patientsList = await _repo.GetAllAsync();

            var filteredList = patientsList.Where(pt => pt.Gender.ToString().Equals(gender)).ToList();

            return _mapper.ToListDto(filteredList);
        }

        /**
         * Retrieves a patient by the specified medical record number.
         *
         * @param id The medical record number of the patient.
         * @return The PatientDto representing the patient with the specified ID, or null if not found.
         */
        public async Task<PatientDto> GetByIdAsync(MedicalRecordNumber id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            return _mapper.ToDto(patient);
        }

        /**
         * Adds a new patient to the repository after verifying the email and phone number uniqueness.
         *
         * @param dto The PatientDto with patient details to add.
         * @return The PatientDto representing the newly added patient.
         * @throws InvalidOperationException if the phone number or email already exists.
         */
        public async Task<PatientDto> AddAsync(PatientDto dto)
        {
            var existingPatient = await GetAllAsync();
            var lastId = existingPatient.Last().MedicalRecordNumber;

            foreach (PatientDto pt in existingPatient)
            {
                if (pt.PhoneNumber.Equals(dto.PhoneNumber, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException(
                        "An patient with the same phone number already exists. Please try with another.");
            }
            
            foreach (PatientDto pt in existingPatient)
            {
                if (pt.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException(
                        "An patient with the same email already exists. Please try with another.");
            }

            if (!string.IsNullOrEmpty(lastId))
            {
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                
                string lastYear = lastId.Substring(0, 4);
                string lastMonth = lastId.Substring(4, 2);
                
                var sequentialNumber = int.Parse(lastId.Substring(6, 6));
                
                if (lastYear == year && lastMonth == month)
                {
                    sequentialNumber++;
                }
                else
                {
                    sequentialNumber = 1;
                }
                
                string seqNumber = sequentialNumber.ToString("D6");
                var finalId = $"{year}{month}{seqNumber}";
                
                dto.MedicalRecordNumber = finalId;
            }
            else
            {
                string year = DateTime.Now.ToString("yyyy");
                string month = DateTime.Now.ToString("MM");
                var sequentialNumber = 1;

                string seqNumber = sequentialNumber.ToString("D6");
                var finalId = $"{year}{month}{seqNumber}";
                
                dto.MedicalRecordNumber = finalId;
            }
            
            var medicalRecordNumber = new MedicalRecordNumber(dto.MedicalRecordNumber);
            
            EmailService emailService = new EmailService();
            const string activationLink = "http://localhost:4200";
            var emailContent = $@"
            <html>
            <body>
              <p>Dear User,</p>
              <p>An admin has successfully registered your account on Surgical Sync.</p>
              <p>You can now activate your account by clicking the button below:</p>
              <a href='{activationLink}' style='display: inline-block; background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Activate Your Account</a>
              <p>Your activation code is: <strong>{medicalRecordNumber.Value}</strong></p>
              <p>If you did not expect this email, please contact our support team immediately.</p>
              <p>Thank you,<br/>The Surgical Sync Team</p>
            </body>
            </html>";
            var email = new Email(emailContent, dto.Email, "Welcome to Surgical Sync!");
            await emailService.SendEmailAsync(email);
            
            var patient = _mapper.ToDomain(dto, medicalRecordNumber, null, null);

            await _repo.AddAsync(patient);
            await _unitOfWork.CommitAsync();

            return _mapper.ToDto(patient);
        }

        /**
         * Updates an existing patient's details in the repository and sends a notification email.
         *
         * @param dto The PatientDto with updated patient details.
         * @return The PatientDto representing the updated patient, or null if the patient was not found.
         */
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
            
            
            //so podem ser alterados por um user com role doctor ou nurse, admin nao pode alterar
            patient.ChangeMedicalConditions(new MedicalConditions(dto.MedicalConditions));
            patient.ChangeAppointmentHistory(new AppointmentHistory(dto.AppointmentHistory));
            
            // Send set-up email to user
            var smtpEmailService = new EmailService();
            var emailContent =
                $"Hello {patient.PatientName}! \n Your  contact information was changed. Now it is : phone number : {phoneNumber}, emergency contact : {emergencyContact} and email : {patientEmail}";
            var email = new Email(emailContent, patient.UserEmail.ToString(), "Changes On Your Contact Information");
            await smtpEmailService.SendEmailAsync(email);
            Console.WriteLine($"Successfully sent the email to {email.Destination}");

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(patient);
        }

        /**
         * Deletes an existing patient from the repository.
         *
         * @param id The medical record number of the patient to delete.
         * @return The PatientDto representing the deleted patient, or null if the patient was not found.
         */
        public async Task<PatientDto> DeleteAsync(MedicalRecordNumber id)
        {
            var patient = await this._repo.GetByIdAsync(id);

            if (patient == null)
                return null;

            this._repo.Remove(patient);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(patient);
        }

        /**
        * Deletes patient data and account in compliance with GDPR.
        *
        * @param id The medical record number of the patient.
        * @return The anonymized PatientDto, or null if not found.
        */
        public async Task RequestDataDeletion(MedicalRecordNumber patientId)
        {
            var patient = await _repo.GetByIdAsync(patientId);

            var email = patient.UserEmail;

            await _deletePatientMicroservice.SendDeletionConfirmationEmail(email);

            var DpoEmail = new UserEmail("1220812@isep.ipp.pt");
            
            var patientDto = _mapper.ToDto(patient);
            
            await _deletePatientMicroservice.SendEmailToDPO(DpoEmail, patientDto);

            await _unitOfWork.CommitAsync();
        }
        
        /**
         * Retrieves the appointment history of a patient by their unique medical record number.
         *
         * @param patientId The medical record number of the patient whose appointment history is requested.
         * @return The AppointmentHistory of the specified patient, or null if the patient or their appointment history is not found.
         * @throws Exception If an error occurs during patient retrieval or appointment history access.
         */

        public async Task<AppointmentHistory> AppointmentHistory(UserEmail patientEmail)
        {
            var patientId = await GetMedicalRecordNumberByUserEmail(patientEmail.ToString());
            if (patientId == null)
            {
                _logger.LogError("Patient not found for email: {email}", patientEmail);
            }

            var patient = await _repo.GetByIdAsync(patientId);
            if (patient == null)
            {
                _logger.LogError("No patient found with the ID: {patientId}", patientId);
            }

            var appointments = await _patientAppointmentHistoryMicroService.GetAllPatientAppointmentHistoryAsync(patientId);
            
            var appointmentHistoryString = string.Join("; ", appointments.Select(a =>
            {
                var day = a.Date.DateTime.ToString("dd");
                var month = a.Date.DateTime.ToString("MM");
                var year = a.Date.DateTime.ToString("yyyy");

                var formattedDate = $"{year}/{month}/{day}";

                return $"{formattedDate}, {a.Time}, {a.Status}";
            }));
            
            return new AppointmentHistory(appointmentHistoryString);
        }
        
        /**
         * Retrieves the MedicalRecordNumber of a patient based on their email address.
         *
         * @param userEmail The email address of the user to find the patient.
         * @return The MedicalRecordNumber associated with the patient.
         * @throws Exception If no patient is found with the specified email address.
         */
        
        public async Task<MedicalRecordNumber> GetMedicalRecordNumberByUserEmail(string userEmail)
        {
            var patients = await _repo.GetAllAsync();
            
            var patient = patients.SingleOrDefault(p => p.UserEmail.ToString().Equals(userEmail, StringComparison.OrdinalIgnoreCase));
            
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            var medicalRecordNumber = patient.Id;

            return medicalRecordNumber;
        }
        
        /**
         * Retrieves the medical history of a patient by their unique medical record number.
         * The medical history includes the patient's name, birth date, gender,
         * phone number, emergency contact, and appointment history.
         * 
         * @param medicalRecordNumber The medical record number of the patient whose medical history is requested.
         * @return The MedicalHistoryDto representing the medical history of the specified patient, or null if the patient is not found.
         */
        public async Task<MedicalHistoryDto> GetMedicalHistoryAsync(string medicalRecordNumber)
        {
            var patient = await _repo.GetByIdAsync(new MedicalRecordNumber(medicalRecordNumber));

            if (patient == null)
            {
                return null;
            }
            
            var patientDto = _mapper.ToDto(patient);

            var medicalHistory = new MedicalHistoryDto
            {
                PatientName = patientDto.PatientName,
                BirthDate = patientDto.BirthDate,
                Gender = patientDto.Gender,
                PhoneNumber = patientDto.PhoneNumber,
                EmergencyContact = patientDto.EmergencyContact,
                AppointmentHistory = patientDto.AppointmentHistory
            };

            return medicalHistory;
        }

    }
}
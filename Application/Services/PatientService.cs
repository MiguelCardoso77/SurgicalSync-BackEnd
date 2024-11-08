using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Shared;
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
            UserEmailMicroService userEmailMicroService, DeletePatientMicroService deletePatientMicroservice)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
            this._userEmailMicroService = userEmailMicroService;
            this._deletePatientMicroservice = deletePatientMicroservice;
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

            var medicalRecordNumber = string.IsNullOrEmpty(dto.MedicalRecordNumber)
                ? new MedicalRecordNumber()
                : new MedicalRecordNumber(dto.MedicalRecordNumber);

            var can = await _userEmailMicroService.VerifyEmail(dto.Email);

            if (can.Equals(false))
            {
                throw new InvalidOperationException("This email already exists. Please try with another.");
            }

            var patient = _mapper.ToDomain(dto, medicalRecordNumber, null, null);

            await this._repo.AddAsync(patient);
            await this._unitOfWork.CommitAsync();

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
        public async Task DeletePatientDataAndAccount(MedicalRecordNumber patientId)
        {
            var patient = await _repo.GetByIdAsync(patientId);

            var email = patient.UserEmail;

            await _deletePatientMicroservice.AnonymizePatientData(patient);

            await _deletePatientMicroservice.DeleteUserAccount(email);

            await _deletePatientMicroservice.SendDeletionConfirmationEmail(email);

            await _unitOfWork.CommitAsync();
        }
        /**
         * Initiates the data deletion process for a patient by sending a confirmation link to the patient's email.
         * This method retrieves the patient by their unique MedicalRecordNumber, and if found, sends a confirmation
         * email allowing the patient to confirm the deletion of their account and personal data.
         *
         * @param patientId The unique MedicalRecordNumber of the patient requesting data deletion.
         * @throws Exception If an error occurs during patient retrieval or email sending.
         *
         * This method utilizes the deletion microservice to send a confirmation email to the patient. The confirmation link.
         * once accessed, allows the deletion process to be completed.
         */
        
        public async Task RequestDeletion(MedicalRecordNumber patientId)
        {
            var patient = await _repo.GetByIdAsync(patientId);

            if (patient != null)
            {
                await _deletePatientMicroservice.SendDeletionConfirmationLink(patient.UserEmail, patientId);
            }
        }
    }
}
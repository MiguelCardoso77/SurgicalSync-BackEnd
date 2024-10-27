using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Microservice responsible for anonymizing and deleting personal data of a patient
     * as required by GDPR, retaining only the medical record number (MedicalRecordNumber)
     * and appointment history (AppointmentHistory).
     */
    public class DeletePatientMicroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepository;
        private readonly UserService _userService;
        private readonly PatientMapper _patientMapper;
        private readonly ILogger<DeletePatientMicroService> _logger;
        /**
         * Initializes a new instance of the DeletePatientMicroService, providing functionality
         * to anonymize patient data in compliance with GDPR and delete the associated account.
         *
         * @param unitOfWork         Unit of work to handle transaction management.
         * @param patientRepository  Repository for accessing and managing patient data.
         * @param patientMapper      Mapper to convert patient entities to Data Transfer Objects (DTOs).
         * @param userService        Service to manage associated user accounts.
         * @param logger             A Logger instance for information and error logging.
         * @param emailService       Service to handle email notifications.
         */
        public DeletePatientMicroService(IUnitOfWork unitOfWork, IPatientRepository patientRepository,
            PatientMapper patientMapper, UserService userService,
            ILogger<DeletePatientMicroService> logger)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
            _patientMapper = patientMapper;
            _userService = userService;
            _logger = logger;
        }

        /**
         * Asynchronously anonymizes personal data for a patient and deletes the associated user account
         * to comply with GDPR. Anonymized data includes name, birthdate, gender, phone number, medical conditions,
         * and emergency contact. Medical record number and appointment history are retained.
         *
         * @param medicalRecordNumber The medical record number of the patient to be anonymized and deleted.
         * @return                    A task representing the asynchronous operation, containing the anonymized Patient DTO
         *                            or null if the patient does not exist.
         * @throws Exception          If an error occurs during the data deletion or email notification process.
         */

        public async Task<PatientDto> DeletePatientDataByGDPRAndAccount(MedicalRecordNumber medicalRecordNumber)
        {
            _logger.LogInformation("Attempting to delete patient data for medical record number: {MedicalRecordNumber}",
                medicalRecordNumber);

            var patient = await _patientRepository.GetByIdAsync(medicalRecordNumber);

            if (patient == null)
            {
                _logger.LogWarning("Patient not found for medical record number: {MedicalRecordNumber}",
                    medicalRecordNumber);
                return null;
            }

            // Create an anonymized version of the patient
            var anonymized = new Patient(
                new PatientName("Anonymous"),
                new BirthDate("1900-01-01"),
                new Gender("Unspecified"),
                patient.Id,
                new PhoneNumber("000-000-0000"),
                new List<MedicalConditions>(),
                new EmergencyContact("000-000-0000"),
                patient.AppointmentHistory,
                new UserEmail("anonymous@domain.com")
            );

            // Remove the original patient record from the repository
            _patientRepository.Remove(patient);
            
            await _patientRepository.AddAsync(anonymized);

            // Commit the deletion
            await _unitOfWork.CommitAsync();
            _logger.LogInformation(
                "Successfully anonymized patient data for medical record number: {MedicalRecordNumber}",
                medicalRecordNumber);

            var userEmail = patient.UserEmail;
            var user = await _userService.GetUserByEmail(userEmail);

            if (user == null)
            {
                _logger.LogInformation("User not found for email: {UserEmail}", userEmail);
                return _patientMapper.ToDto(anonymized); // Return anonymized patient even if user is not found
            }

            var userId = new UserId(user.Id);
            try
            {
                var deletedUserDto = await _userService.DeleteAsync(userId);
                await _unitOfWork.CommitAsync(); // Commit after deletion

                if (deletedUserDto != null)
                {
                    _logger.LogInformation("Successfully deleted user account for email: {UserEmail}", userEmail);
                }
                else
                {
                    _logger.LogError("Failed to delete user account for email: {UserEmail}", userEmail);
                    return _patientMapper.ToDto(anonymized);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during deletion of user account for email: {UserEmail}", userEmail);
                return _patientMapper.ToDto(anonymized); // Return anonymized DTO if delete fails
            }

            // Send email notification
            try
            {
                EmailService emailService = new EmailService();
                var emailContent = $"Hello,\n\n" +
                                   "We are reaching out to inform you that your personal data and account have been permanently deleted from the SurgicalSync System, as per your request.\n\n" +
                                   "Your account information, including any identifiable medical records, appointment history, and other personal data, has been securely erased in compliance with GDPR regulations. Some anonymized data may be retained for legal or research purposes; however, this data cannot be linked back to you.\n\n" +
                                   "Thank you for your trust in SurgicalSync.\n\n" +
                                   "Best regards,\n" +
                                   "The SurgicalSync Team";

                var email = new Email(emailContent, userEmail.ToString(),
                    "Your Account and Data Have Been Successfully Deleted");
                await emailService.SendEmailAsync(email);

                _logger.LogInformation("Successfully deleted account and associated data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error occurred while attempting to delete account and associated data for email: {Email}",
                    userEmail);
            }

            return _patientMapper.ToDto(anonymized);
        }
    }
}
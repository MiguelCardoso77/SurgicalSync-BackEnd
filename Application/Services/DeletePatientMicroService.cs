using System;
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
        
        public DeletePatientMicroService(IUnitOfWork unitOfWork, IPatientRepository patientRepository,
            PatientMapper patientMapper, UserService userService, ILogger<DeletePatientMicroService> logger)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
            _patientMapper = patientMapper;
            _userService = userService;
            _logger = logger;
        }
        /**
         * Sends a confirmation link to the user's email for confirming the account and data deletion request.
         *
         * @param userEmail The email address of the user requesting the deletion.
         */
        public async Task SendDeletionConfirmationLink(UserEmail userEmail, MedicalRecordNumber patientId)
        {
            var link = $"https://localhost:5001/api/patients/confirm-deletion/{patientId}";
            var emailContent = $"Hello,\n\n" +
                               "We've received your request for account and data deletion.\n\n" +
                               $"Please confirm your decision by clicking on the following link:\n{link}\n\n" +
                               "Thank you for your trust in SurgicalSync.\n\n" +
                               "Best regards,\n" +
                               "The SurgicalSync Team";

            var email = new Email(emailContent, userEmail.ToString(), "Account and data deletion");
            var emailService = new EmailService();
            await emailService.SendEmailAsync(email);
            _logger.LogInformation("Successfully sent deletion notification to: {UserEmail}", userEmail);
        }
        /**
         * Anonymizes a patient's personal data while retaining their medical record number and appointment history.
         *
         * @param patient The patient whose data is to be anonymized.
         */
        public async Task AnonymizePatientData(Patient patient)
        {
            var anonymized = new Patient(
                new PatientName("Anonymous"),
                new BirthDate("1900-01-01"),
                new Gender("Unspecified"),
                patient.Id,
                new PhoneNumber("000-000-0000"),
                new MedicalConditions("Null"),
                new EmergencyContact("000-000-0000"),
                patient.AppointmentHistory,
                new UserEmail("anonymous@domain.com")
            );
            
            _patientRepository.Remove(patient);
            await _patientRepository.AddAsync(anonymized);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Successfully anonymized patient data for medical record number: {MedicalRecordNumber}", patient.Id);
        }
        /**
         * Deletes a user account and its associated data.
         *
         * @param userId The unique identifier of the user to be deleted.
         * @param userEmail The email address of the user to be deleted.
         */
        public async Task DeleteUserAccount(UserEmail userEmail)
        {
            try
            {
                
                var user = await _userService.GetUserByEmail(userEmail);

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
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception during deletion of user account for email: {UserEmail}", userEmail);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during deletion of user account for email: {UserEmail}", userEmail);
            }
        }
        /**
         * Sends a confirmation email to the user after their account and data have been permanently deleted.
         *
         * @param userEmail The email address of the user whose data has been deleted.
         */
        public async Task SendDeletionConfirmationEmail(UserEmail userEmail)
        {
            var emailContent = $"Hello,\n\n" +
                               "Your personal data and account have been permanently deleted as requested.\n\n" +
                               "Thank you for your trust in SurgicalSync.\n\n" +
                               "Best regards,\n" +
                               "The SurgicalSync Team";

            var email = new Email(emailContent, userEmail.ToString(), "Your Account and Data Have Been Deleted");
            var emailService = new EmailService();
            await emailService.SendEmailAsync(email);
            _logger.LogInformation("Successfully sent deletion notification to: {UserEmail}", userEmail);
        }
    }
}
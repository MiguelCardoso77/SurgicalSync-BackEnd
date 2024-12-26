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
         * Sends a confirmation email to the user's email confirming the account and data deletion request.
         *
         * @param userEmail The email address of the user requesting the deletion.
         */
        public async Task SendDeletionConfirmationEmail(UserEmail userEmail)
        {
            var emailContent = $"Hello,\n\n" +
                               "We've received your request for account and data deletion.\n\n" +
                               "Thank you for your trust in SurgicalSync.\n\n" +
                               "Best regards,\n" +
                               "The SurgicalSync Team";

            var email = new Email(emailContent, userEmail.ToString(), "Account and data deletion");
            var emailService = new EmailService();
            await emailService.SendEmailAsync(email);
            _logger.LogInformation("Successfully sent deletion notification to: {UserEmail}", userEmail);
        }
        /**
         * Sends an email to the DPO requesting the review of the account and data request.
         *
         * @param userEmail The email address of the DPO.
         */
        public async Task SendEmailToDPO(UserEmail userEmail, PatientDto patient)
        {
            var emailContent = $"Hello,\n\n" +
                               "We have sent the data of a deletion request, awaiting your review. Please contact a system administrator once your inspection is complete.\n\n" +
                               "Patient Data:\n" +
                               $"Name: {patient.PatientName}\n" +
                               $"BirthDate: {patient.BirthDate}\n" +
                               $"Gender: {patient.Gender}\n" +
                               $"MedicalRecordNumber: {patient.MedicalRecordNumber}\n" +
                               $"PhoneNumber: {patient.PhoneNumber}\n" +
                               $"MedicalRecord: {patient.MedicalConditions}\n" +
                               $"EmergencyContact: {patient.EmergencyContact}\n" +
                               $"AppointmentHistory: {patient.AppointmentHistory}\n" +
                               $"Email: {patient.Email}\n" +
                               "\n\nThank you!\n\n" +
                               "Best regards,\n" +
                               "The SurgicalSync Team";

            var email = new Email(emailContent, userEmail.ToString(), "Account and data deletion");
            var emailService = new EmailService();
            await emailService.SendEmailAsync(email);
            _logger.LogInformation("Successfully sent deletion notification to: {UserEmail}", userEmail);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.Exceptions;

namespace DDDNetCore.Controllers
{
    /**
     * Controller for managing patient-related operations.
     * Provides endpoints for retrieving, creating, updating, and deleting patient records.
     */
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _service;

        /**
         * Initializes a new instance of the PatientsController class.
         *
         * @param service The patient service used for managing patient records.
         */
        public PatientsController(PatientService service)
        {
            _service = service;
        }

        /**
         * Retrieves all patients or filters patients based on the provided query parameters.
         *
         * @param patientName Optional; the name of the patient to filter by.
         * @param birthDate Optional; the birth date of the patient to filter by.
         * @param userEmail Optional; the email address to filter by.
         * @param phoneNumber Optional; the phone number to filter by.
         * @param gender Optional; the gender to filter by.
         * @return An ActionResult containing a list of PatientDto objects.
         */
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll([FromQuery] string patientName = null,
            [FromQuery] string birthDate = null, [FromQuery] string userEmail = null,
            [FromQuery] string phoneNumber = null, [FromQuery] string gender = null)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

            if (!string.IsNullOrEmpty(patientName))
            {
                var result = await _service.GetAllByPatientNameAsync(patientName);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(birthDate))
            {
                var result = await _service.GetAllByBirthDateAsync(birthDate);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                var result = await _service.GetAllByEmailAsync(userEmail);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                var result = await _service.GetAllByPhoneNumberAsync(phoneNumber);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                var result = await _service.GetAllByGenderAsync(gender);
                return Ok(result);
            }

            var allPatients = await _service.GetAllAsync();
            Console.WriteLine("All patient profiles listed successfully.");
            return Ok(allPatients);
        }

        /**
         * Retrieves a patient by their medical record number.
         *
         * @param id The medical record number of the patient to retrieve.
         * @return An ActionResult containing the PatientDto object if found, otherwise NotFound.
         */
        // GET: api/Patients/P1
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(string id)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

            var pat = await _service.GetByIdAsync(new MedicalRecordNumber(id));

            if (pat == null)
            {
                return NotFound();
            }
            Console.WriteLine($"Patient with Medical Record Number = {id} was retrieved successfully.");
            return pat;
        }

        /**
         * Creates a new patient record.
         *
         * @param dto The patient data transfer object containing patient information.
         * @return An ActionResult containing the created PatientDto object.
         */
        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(PatientDto dto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

            var pat = await _service.AddAsync(dto);

            if (string.IsNullOrWhiteSpace(pat.MedicalRecordNumber))
            {
                return BadRequest("Failed to generate a valid Medical Record Number.");
            }
            
            Console.WriteLine($"Patient with Medical Record Number Controller = {dto.MedicalRecordNumber} was created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = pat.MedicalRecordNumber }, pat);
        }

        /**
         * Updates an existing patient record.
         *
         * @param id The medical record number of the patient to update.
         * @param dto The updated patient data transfer object.
         * @return An ActionResult containing the updated PatientDto object if successful, otherwise NotFound or BadRequest.
         */
        // PUT: api/Patients/P5
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(string id, PatientDto dto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            if (id != dto.MedicalRecordNumber)
            {
                return BadRequest();
            }

            try
            {
                var pat = await _service.UpdateAsync(dto);

                if (pat == null)
                {
                    return NotFound();
                }
                Console.WriteLine($"Patient with Medical Record Number = {id} was updated successfully.");
                return pat;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /**
         * Deletes a patient record by their medical record number.
         *
         * @param id The medical record number of the patient to delete.
         * @return An ActionResult containing the deleted PatientDto object if successful, otherwise NotFound.
         */
        // DELETE: api/Patients/P5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientDto>> Delete(string id)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var pat = await _service.DeleteAsync(new MedicalRecordNumber(id));

            if (pat == null)
            {
                return NotFound();
            }
            Console.WriteLine($"Patient with Medical Record Number = {id} was deleted successfully.");
            return Ok(pat);
        }
        
        
        [HttpDelete("request-deletion/{patientId}")]
        public async Task<ActionResult> RequestDeletion(string patientId)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var medicalRecordNumber = new MedicalRecordNumber(patientId);
            var patient = await _service.GetByIdAsync(medicalRecordNumber);

            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            await _service.RequestDataDeletion(medicalRecordNumber);

            return Ok("Deletion confirmation email sent.");
        }

        [HttpGet("appointmentHistory/patientEmail")]
        public async Task<ActionResult<AppointmentHistory>> AppointmentHistory([FromQuery] string email)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email cannot be null or empty.");
            }

            var appointmentHistory = await _service.AppointmentHistory(new UserEmail(email));

            if (appointmentHistory == null)
            {
                return NotFound("Appointment history not found for the given email.");
            }

            return Ok(appointmentHistory);
        }

        [HttpGet("medicalRecordNumber")]
        public async Task<ActionResult<MedicalRecordNumber>> GetMedicalRecordNumberByEmail([FromQuery] string email)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email cannot be null or empty.");
            }
            var medicalRecordNumber = await _service.GetMedicalRecordNumberByUserEmail(email);

            if (medicalRecordNumber == null)
            {
                return NotFound("Medical record number not found for the given email.");
            }

            return Ok(medicalRecordNumber);
        }
        
        /**
         * Retrieves the medical history of a patient by their medical record number.
         *
         * @param id The medical record number of the patient to retrieve the medical history for.
         * @return An ActionResult containing the MedicalHistoryDto object if found, otherwise NotFound.
         */
        [HttpGet("{email}/medical-history")]
        public async Task<ActionResult<MedicalHistoryDto>> GetMedicalHistory(string email)
        {
            var medicalHistory = await _service.GetMedicalHistoryAsync(email);
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

            var medicalHistory = await _service.GetMedicalHistoryAsync(id);

            if (medicalHistory == null)
            {
                return NotFound("Medical history not found for the given medical record number.");
            }

            return Ok(medicalHistory);
        }
        
        private bool AuthorizeRequest()
        {
            var authorizationHeader = Request.Headers.Authorization.FirstOrDefault();
            const string validAuthorizationToken = "ICcTTh51IzOiBKmftT1SnrBH5d42";
        
            if (string.IsNullOrEmpty(authorizationHeader) || authorizationHeader != validAuthorizationToken)
            {
                return false;
            }
            
            return true;
        }
    }
}
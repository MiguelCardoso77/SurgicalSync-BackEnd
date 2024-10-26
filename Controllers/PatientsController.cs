using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Patients;
using Microsoft.AspNetCore.Mvc;

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
         * @param medicalRecordNumber Optional; the medical record number to filter by.
         * @param userEmail Optional; the email address to filter by.
         * @param phoneNumber Optional; the phone number to filter by.
         * @param gender Optional; the gender to filter by.
         * @return An ActionResult containing a list of PatientDto objects.
         */
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll([FromQuery] string patientName = null,
            [FromQuery] string birthDate = null, [FromQuery] string medicalRecordNumber = null,
            [FromQuery] string userEmail = null, [FromQuery] string phoneNumber = null,
            [FromQuery] string gender = null)
        {
            if (!string.IsNullOrEmpty(patientName))
            {
                var result = await _service.GetAllByPatientName(patientName);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(birthDate))
            {
                var result = await _service.GetAllByBirthDate(birthDate);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(medicalRecordNumber))
            {
                var result = await _service.GetAllByMedicalRecordNumber(medicalRecordNumber);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                var result = await _service.GetAllByEmail(userEmail);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                var result = await _service.GetAllByPhoneNumber(phoneNumber);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                var result = await _service.GetAllByGender(gender);
                return Ok(result);
            }

            var allPatients = await _service.GetAllAsync();
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
            var pat = await _service.GetByIdAsync(new MedicalRecordNumber(id));

            if (pat == null)
            {
                return NotFound();
            }

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
            var pat = await _service.AddAsync(dto);

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
            var pat = await _service.DeleteAsync(new MedicalRecordNumber(id));

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }
        
        // DELETE: api/Patients/GDPR/P5
        [HttpDelete("GDPR/{id}")]
        public async Task<ActionResult<PatientDto>> DeletePatientDataAndAccount(string id)
        {
            var pat = await _service.DeletePatientDataAndAccount(new MedicalRecordNumber(id));

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }
    }
}
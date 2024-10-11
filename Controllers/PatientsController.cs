using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Patients;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _service;
        
        public PatientsController(PatientService service)
        {
            _service = service;
        }
        
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }
        
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
        
        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(PatientDto dto)
        {
            var pat = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = pat.MedicalRecordNumber}, pat);
        }
        
        // PUT: api/Patients/P5
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> Update(String id, PatientDto dto)
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
        
        // DELETE: api/Patients/P5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var pat = await _service.DeleteAsync(new MedicalRecordNumber(id));

                if (pat == null)
                {
                    return NotFound();
                }

                return Ok(pat);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
    }
}
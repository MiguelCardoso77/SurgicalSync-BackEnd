using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Patients;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationRequestsController : ControllerBase
    {
        private readonly OperationRequestService _service;

        public OperationRequestsController(OperationRequestService service)
        {
            _service = service;
        }
        
        // GET: api/OperationRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> GetAll(
            [FromQuery] string medicalRecordNumber = null,
            [FromQuery] string startDate = null, [FromQuery] string endDate = null, [FromQuery] bool? isActive = null,
            [FromQuery] string patientName = null, [FromQuery] string operationTypeName = null)
        {
            if (!string.IsNullOrEmpty(medicalRecordNumber))
            {
                var result = await _service.GetAllByMedicalRecordNumber(medicalRecordNumber);
                return Ok(result);
            }

            if (isActive.HasValue)
            {
                var result = await _service.GetAllByStatus(isActive.Value);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                var result = await _service.GetAllInsideDateRange(startDate, endDate);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(patientName))
            {
                var result = await _service.GetAllByPatientName(patientName);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(operationTypeName))
            {
                var result = await _service.GetAllByOperationName(operationTypeName);
                return Ok(result);
            }
            
            var allRequests = await _service.GetAllAsync();
            return Ok(allRequests);
        }
        
        // GET: api/OperationRequests/OR1
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationRequestDto>> GetById(String id)
        {
            var or = await _service.GetByIdAsync(new OperationRequestId(id));
            
            if (or == null)
            {
                return NotFound();
            }
            
            return or;
        }
        
        // POST: api/OperationRequests
        [HttpPost]
        public async Task<ActionResult<OperationRequestDto>> Create(OperationRequestDto dto)
        {
            var or = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = or.OperationRequestId}, or);
        }
        
        // PUT: api/OperationRequests/OR5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationRequestDto>> Update(String id, OperationRequestDto dto)
        {
            if (id != dto.OperationRequestId)
            {
                return BadRequest();
            }
            
            var or = await _service.UpdateAsync(dto);

            if (or == null)
            {
                return NotFound();
            }

            return or;
        }
        
        // DELETE: api/OperationRequests/OR5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationRequestDto>> Delete(String id)
        {
            Console.WriteLine("Do you really wish to erase this operation request from the system?");
            
            var oR = await _service.InactivateAsync(new OperationRequestId(id));

            if (oR == null)
            {
                return NotFound();
            }

            return Ok(oR);
        }
    }
}
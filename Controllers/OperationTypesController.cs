using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationTypes;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        private readonly OperationTypeService _service;
        
        public OperationTypesController(OperationTypeService service)
        {
            _service = service;
        }
        
        // GET: api/OperationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDto>>> GetAll([FromQuery] string specialization = null, [FromQuery] string operationName = null, [FromQuery] bool? isActive = null)
        {
            if (!string.IsNullOrEmpty(specialization))
            {
                var result = await _service.GetAllBySpecialization(specialization);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(operationName))
            {
                var result = await _service.GetAllByName(operationName);
                return Ok(result);
            }

            if (isActive.HasValue)
            {
                var result = await _service.GetAllByStatus(isActive.Value);
                return Ok(result);
            }
            
            var allOperations = await _service.GetAllAsync();
            return Ok(allOperations);
        }
        
        // GET: api/OperationTypes/OT1
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDto>> GetById(string id)
        {
            var oT = await _service.GetByIdAsync(new OperationTypeId(id));
            
            if (oT == null)
            {
                return NotFound();
            }
            
            return oT;
        }
        
        // POST: api/OperationTypes
        [HttpPost]
        public async Task<ActionResult<OperationTypeDto>> Create(OperationTypeDto dto)
        {
            var task = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
        
        // PUT: api/OperationTypes/OT5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDto>> Update(string id, OperationTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            
            var oT = await _service.UpdateAsync(dto);
            
            if (oT == null)
            {
                return NotFound();
            }
            
            return oT;
        }
        
        // DELETE: api/OperationTypes/OT5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationTypeDto>> Delete(string id)
        {
            // Confirmation for deletion example, can't be obtained without UI.
            Console.WriteLine("Do you really wish to erase this operation type from the system?");
            
            var oT = await _service.InactivateAsync(new OperationTypeId(id));

            if (oT == null)
            {
                return NotFound();
            }

            return Ok(oT);
        }
        
    }
}
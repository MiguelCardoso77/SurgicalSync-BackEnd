using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationType;
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
            Console.WriteLine("All operation types listed successfully.");
            return Ok(allOperations);
        }
        
        // GET: api/OperationTypes/OTID
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDto>> GetById(string id)
        {
            var oT = await _service.GetByIdAsync(new OperationTypeId(id));
            
            if (oT == null)
            {
                return NotFound();
            }
            
            Console.WriteLine($"Operation type with ID = {id} was retrieved successfully.");
            return oT;
        }
        
        // POST: api/OperationTypes
        [HttpPost]
        public async Task<ActionResult<OperationTypeDto>> Create(OperationTypeDto dto)
        {
            var task = await _service.AddAsync(dto);
            
            Console.WriteLine($"Operation type with ID = {dto.Id} was created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
        
        // PUT: api/OperationTypes/OTID
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDto>> Update(string id, OperationTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            Console.WriteLine(dto.OperationName);
            Console.WriteLine(dto.RequiredStaff);
            Console.WriteLine(dto.EstimatedDuration);
            
            var oT = await _service.UpdateAsync(dto);
            
            if (oT == null)
            {
                return NotFound();
            }
            
            Console.WriteLine($"Operation type with ID = {id} was updated successfully.");
            return oT;
        }
        
        // DELETE: api/OperationTypes/OTID
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationTypeDto>> Delete(string id)
        {
            var oT = await _service.InactivateAsync(new OperationTypeId(id));
            
            if (oT == null)
            {
                return NotFound();
            }

            Console.WriteLine($"Operation type with ID = {id} was deleted successfully.");
            return Ok(oT);
        }
        
    }
}
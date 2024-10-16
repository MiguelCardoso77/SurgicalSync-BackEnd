using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationRequests;
using DDDSample1.Domain.Shared;
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
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> GetAll()
        {
            return await _service.GetAllAsync();
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

            try
            {
                var or = await _service.UpdateAsync(dto);

                if (or == null)
                {
                    return NotFound();
                }

                return or;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE: api/OperationRequests/OR5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationRequestDto>> Delete(String id)
        {
            try
            {
                var or = await _service.InactivateAsync(new OperationRequestId(id));

                if (or == null)
                {
                    return NotFound();
                }

                return Ok(or);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
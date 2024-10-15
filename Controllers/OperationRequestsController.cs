using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.OperationRequests;
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
            var ot = await _service.GetByIdAsync(new OperationRequestId(id));
            
            if (ot == null)
            {
                return NotFound();
            }
            
            return ot;
        }
        
        // POST: api/OperationRequests
        [HttpPost]
        public async Task<ActionResult<OperationRequestDto>> Create(OperationRequestDto dto)
        {
            var task = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = task.OperationRequestId}, task);
        }
        
        // PUT: api/OperationRequests/OR5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationRequestDto>> Update(String id, OperationRequestDto dto)
        {
            //if (id != dto.Id)
            {
                return BadRequest();
            }
            
            var ot = await _service.UpdateAsync(dto);
            
            if (ot == null)
            {
                return NotFound();
            }
            
            return ot;
        }
        
        // DELETE: api/OperationRequests/OR5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationRequestDto>> Delete(Guid id)
        {
            // Do you really wish to erase this operation type from the system?
            var oT = await _service.InactivateAsync(new OperationRequestId(id.ToString()));

            if (oT == null)
            {
                return NotFound();
            }

            return Ok(oT);
        }
    }
}
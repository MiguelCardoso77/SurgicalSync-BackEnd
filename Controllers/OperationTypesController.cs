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
        public async Task<ActionResult<IEnumerable<OperationTypeDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }
        
        // GET: api/OperationTypes/OT1
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDto>> GetById(String id)
        {
            var ot = await _service.GetByIdAsync(new OperationTypeId(id));
            
            if (ot == null)
            {
                return NotFound();
            }
            
            return ot;
        }
        
        // POST: api/OperationTypes
        [HttpPost]
        public async Task<ActionResult<OperationTypeDto>> Create(OperationTypeDto dto)
        {
            var ot = await _service.AddAsync(dto);
            
            //return CreatedAtAction(nameof(GetById), new { id = ot.Id }, ot);
            return null;
        }
        
        // PUT: api/OperationTypes/OT5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDto>> Update(String id, OperationTypeDto dto)
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
        
        // DELETE: api/OperationTypes/OT5
        public async Task<ActionResult<OperationTypeDto>> Delete(String id)
        {
            var ot = await _service.RemoveAsync(new OperationTypeId(id));
            
            if (ot == null)
            {
                return NotFound();
            }
            
            return ot;
        }
        
    }
}
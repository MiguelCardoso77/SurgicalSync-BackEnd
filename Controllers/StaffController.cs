using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    { 
    
        private readonly StaffService _service;
        
        public StaffController(StaffService service)
        {
            _service = service;
        }
        
        // GET: api/staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        
        
        // GET: api/staff/st1
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetById(string id)
        {
            var sT = await _service.GetByIdAsync(new LicenseNumber(id));
            
            if (sT == null)
            {
                return NotFound();
            }
            
            return sT;
        }
        
        // POST: api/Stff
        [HttpPost]
        public async Task<ActionResult<StaffDto>> AddAsync(StaffDto dto)
        {
            var staff = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff);
        }
        
        // PUT: api/Staff/S5
        [HttpPut("{id}")]
        public async Task<ActionResult<StaffDto>> Update(String id, StaffDto dto)
        {
            if (id != dto.Id)
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
        
        // DELETE: api/Staff/S6
        [HttpDelete("{id}")]
        public async Task<ActionResult<StaffDto>> Delete(string id)
        {
            // Confirmation for deletion example, can't be obtained without UI.
            Console.WriteLine("Do you really wish to erase this operation type from the system?");
            
            var oT = await _service.DeleteAsync(new LicenseNumber(id));

            if (oT == null)
            {
                return NotFound();
            }

            return Ok(oT);
        }
        
        /// DEACTIVATE: api/Staff/Deactivate/S6
        [HttpPut("Deactivate/{id}")]
        public async Task<ActionResult<StaffDto>> Deactivate(string id)
        {
            // Confirmando desativação, não pode ser obtido sem interface.
            Console.WriteLine("Deseja realmente desativar este usuário do sistema?");
    
            // Chamando o serviço para desativar o usuário, assumindo que a lógica de desativação seja implementada no serviço.
            var result = await _service.DeactivateAsync(new LicenseNumber(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
            
        }   
        
    }

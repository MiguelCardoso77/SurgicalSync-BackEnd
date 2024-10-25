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
        
        
        // GET: api/staff/st1
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetById(string id)
        {
            var sT = await _service.GetByIdAsync(new StaffId(id));
            
            if (sT == null)
            {
                return NotFound();
            }
            
            return sT;
        }
        
        // GET: api/OperationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAll([FromQuery] string staffName = null, [FromQuery] string staffEmail = null, [FromQuery] string  StaffSpecialization = null)
        {
            if (!string.IsNullOrEmpty(StaffSpecialization))
            {
                var result = await _service.GetAllBySpecialization(StaffSpecialization);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(staffName))
            {
                var result = await _service.GetAllByName(staffName);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(staffEmail))
            {
                var result = await _service.GetAllByEmail(staffEmail);
                return Ok(result);
            }
            if(string.IsNullOrEmpty(StaffSpecialization)&& string.IsNullOrEmpty(staffName) && string.IsNullOrEmpty(staffEmail))
            {
                var allStaff = await _service.GetAllAsync();
                return Ok(allStaff);
            }

            return null;
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
        
        /*// DELETE: api/Staff/S6
        [HttpDelete("{id}")]
        public async Task<ActionResult<StaffDto>> Delete(string id)
        {
            // Confirmation for deletion example, can't be obtained without UI.
            Console.WriteLine("Do you really wish to erase this operation type from the system?");
            
            var oT = await _service.DeleteAsync(new StaffId(id));

            if (oT == null)
            {
                return NotFound();
            }

            return Ok(oT);
        }
        */
        /// DEACTIVATE: api/Staff/Deactivate/S6
        [HttpDelete("{id}")]        
        public async Task<ActionResult<StaffDto>> Deactivate(string id)
        {
    
            // Chamando o serviço para desativar o usuário, assumindo que a lógica de desativação seja implementada no serviço.
            var result = await _service.DeactivateAsync(new StaffId(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
            
        }   
        
    }

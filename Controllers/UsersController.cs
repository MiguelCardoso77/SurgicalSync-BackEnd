using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Users;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        
        public UsersController(UserService service)
        {
            _service = service;
        }
        
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }
        
        // GET: api/Users/U1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(String id)
        {
            var user = await _service.GetByIdAsync(new UserId(id));
            
            if (user == null)
            {
                return NotFound();
            }
            
            return user;
        }
        
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserDto dto)
        {
            var user = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        
        // PUT: api/Users/U5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(String id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            
            try
            {
                var user = await _service.UpdateAsync(dto);
                
                if (user == null)
                {
                    return NotFound();
                }
                
                return user;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE: api/Users/U5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var fam = await _service.DeleteAsync(new UserId(id));

                if (fam == null)
                {
                    return NotFound();
                }

                return Ok(fam);
            }
            catch(BusinessRuleValidationException ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
    }
}
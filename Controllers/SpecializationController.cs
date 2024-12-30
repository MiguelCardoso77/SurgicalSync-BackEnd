using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Specializations;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationController: ControllerBase
{
    private readonly SpecializationService _service;
    
    public SpecializationController(SpecializationService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll([FromQuery] string designation = null)
    {
<<<<<<< Updated upstream
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
=======
>>>>>>> Stashed changes
        if (!string.IsNullOrEmpty(designation))
        {
            var result = await _service.GetByDesignationAsync(designation);
            return Ok(result);
        }
<<<<<<< Updated upstream

        var allSpecializations = await _service.GetAllAsync();
        Console.WriteLine("All specialization listed successfully.");
        return Ok(allSpecializations);
    }
        
    // GET: api/Specializations/SC
    [HttpGet("{code}")]
    public async Task<ActionResult<SpecializationDto>> GetByCode(string code)
    {
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        var specialization = await _service.GetByCodeAsync(code);
        Console.WriteLine($"Specialization with Code = {code} was retrieved successfully.");
        return Ok(specialization);
    }
    
=======
        
        if(string.IsNullOrEmpty(designation))
        {
            var allSpecialization = await _service.GetAllAsync();
            Console.WriteLine("All specializations listed successfully!");
            return Ok(allSpecialization);
        }

        return null;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SpecializationDto>> GetById(string id)
    {
        var sT = await _service.GetByIdAsync(new SpecializationCode(id));

        if (sT == null)
        {
            return NotFound();
        }

        return sT;
    }

>>>>>>> Stashed changes
        
    // POST: api/Specializations
    [HttpPost]
    public async Task<ActionResult<SpecializationDto>> AddAsync(SpecializationDto specializationDto)
    {
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        var createdSpecialization = await _service.AddAsync(specializationDto);
        Console.WriteLine($"Specialization with Code = {specializationDto.code} was created successfully.");
        return CreatedAtAction(nameof(GetById), new { code = createdSpecialization.code }, createdSpecialization);
    }

<<<<<<< Updated upstream
    [HttpPut("{code}")]
    public async Task<ActionResult<SpecializationDto>> UpdateAsync(String code, SpecializationDto dto)
    {
        
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        if (code != dto.SpecializationCode)
=======
    // POST: api/Specializations
    [HttpPut("{id}")]
    public async Task<ActionResult<SpecializationDto>> UpdateAsync(String code, SpecializationDto dto)
    {
        if (code != dto.code)
>>>>>>> Stashed changes
        {
            return BadRequest();
        }

        var ot = await _service.UpdateAsync(dto);

        if (ot == null)
        {
            return NotFound();
        }
        Console.WriteLine($"Specialization with code = {code} was updated successfully.");


        return ot;
        
    }
    
    // DELETE: api/Patients/P5
    [HttpDelete("{id}")]
    public async Task<ActionResult<PatientDto>> Delete(string id)
    {
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        var pat = await _service.DeleteAsync(new SpecializationId(id));

<<<<<<< Updated upstream
        if (pat == null)
        {
            return NotFound();
        }
        Console.WriteLine($"Specialization with specialization code  = {id} was deleted successfully.");
        return Ok(pat);
    }

    private bool AuthorizeRequest()
    {
        var authorizationHeader = Request.Headers.Authorization.FirstOrDefault();
        const string validAuthorizationToken = "ICcTTh51IzOiBKmftT1SnrBH5d42";
        
        if (string.IsNullOrEmpty(authorizationHeader) || authorizationHeader != validAuthorizationToken)
        {
            return false;
        }
            
        return true;
=======
    // DELETE: api/Specializations/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<SpecializationDto>> DeleteAsync(string id)
    {
        var deletedSpecialization = await _service.DeleteAsync(new SpecializationCode(id));

        if (deletedSpecialization == null)
        {
            return NotFound();
        }

        Console.WriteLine($"Specialization with Code = {id} was deleted successfully.");
        return Ok(deletedSpecialization);  
>>>>>>> Stashed changes
    }

}
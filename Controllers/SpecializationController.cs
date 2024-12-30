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
    
    // GET: api/Specializations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll([FromQuery] string designation = null)
    {
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        if (!string.IsNullOrEmpty(designation))
        {
            var result = await _service.GetByDesignationAsync(designation);
            return Ok(result);
        }

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
    
        
    // POST: api/Specializations
    [HttpPost]
    public async Task<ActionResult<SpecializationDto>> AddAsync(SpecializationDto specializationDto)
    {
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        var createdSpecialization = await _service.AddAsync(specializationDto);
        Console.WriteLine($"Specialization with Code = {specializationDto.SpecializationCode} was created successfully.");
        return CreatedAtAction(nameof(GetByCode), new { code = createdSpecialization.SpecializationCode }, createdSpecialization);
    }

    [HttpPut("{code}")]
    public async Task<ActionResult<SpecializationDto>> UpdateAsync(String code, SpecializationDto dto)
    {
        
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        if (code != dto.SpecializationCode)
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
    }

}
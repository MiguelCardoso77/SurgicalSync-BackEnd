using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Specializations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using Org.BouncyCastle.Asn1.Ocsp;

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
        
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SpecializationDto>> GetById(string id)
    {
        var sT = await _service.GetByCodeAsync(id);

        if (sT == null)
        {
            return NotFound();
        }

        return sT;
    }

        
    // POST: api/Specializations
    [HttpPost]
    public async Task<ActionResult<SpecializationDto>> AddAsync(SpecializationDto specializationDto)
    {
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
        
        var createdSpecialization = await _service.AddAsync(specializationDto);
        Console.WriteLine($"Specialization with Code = {specializationDto.code} was created successfully.");
        return CreatedAtAction(nameof(GetById), new { id = createdSpecialization.code }, createdSpecialization);
    }

     
    // PUT: api/Specializations
    [HttpPut("{id}")]
    public async Task<ActionResult<SpecializationDto>> UpdateAsync(String id, SpecializationDto dto)
    {
        
        if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }

       
        if (!id.Equals(dto.code))
        {
            return BadRequest();
        }
        
        var ot = await _service.UpdateAsync(dto);

        if (ot == null)
        {
            return NotFound();
        }
        Console.WriteLine($"Specialization with code = {id} was updated successfully.");


        return ot;
        
    } 
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
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
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetAll()
    {
        var allSpecializations = await _service.GetAllAsync();
        Console.WriteLine("All specialization listed successfully.");
        return Ok(allSpecializations);
    }
        
    // GET: api/Specializations/SC
    [HttpGet("{code}")]
    public async Task<ActionResult<SpecializationDto>> GetByCode(string code)
    {
        var specialization = await _service.GetByCodeAsync(code);
        Console.WriteLine($"Specialization with Code = {code} was retrieved successfully.");
        return Ok(specialization);
    }
    
    // GET: api/Specializations/SD
    [HttpGet("{designation}")]
    public async Task<ActionResult<SpecializationDto>> GetByDesignation(string designation)
    {
        var specialization = await _service.GetByDesignationAsync(designation);
        Console.WriteLine($"Specialization with Code = {designation} was retrieved successfully.");
        return Ok(specialization);
    }
        
    // POST: api/Specializations
    [HttpPost]
    public async Task<ActionResult<SpecializationDto>> AddAsync(SpecializationDto specializationDto)
    {
        var createdSpecialization = await _service.AddAsync(specializationDto);
        Console.WriteLine($"Specialization with Code = {specializationDto.SpecializationCode} was created successfully.");
        return CreatedAtAction(nameof(GetByCode), new { code = createdSpecialization.SpecializationCode }, createdSpecialization);
    }

    public async Task<ActionResult<SpecializationDto>> UpdateAsync(String code, SpecializationDto dto)
    {
        if (code != dto.SpecializationCode)
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


}
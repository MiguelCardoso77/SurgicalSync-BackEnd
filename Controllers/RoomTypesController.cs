using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly RoomTypeService _service;
        
        public RoomTypesController(RoomTypeService service)
        {
            _service = service;
        }
        
        // GET: api/RoomTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeDto>>> GetAll()
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var allRoomTypes = await _service.GetAllAsync();
            Console.WriteLine("All room types listed successfully.");
            return Ok(allRoomTypes);
        }
        
        // GET: api/RoomTypes/RTC
        [HttpGet("{code}")]
        public async Task<ActionResult<RoomTypeDto>> GetByCode(string code)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var roomType = await _service.GetByCodeAsync(code);
            Console.WriteLine($"Room type with Code = {code} was retrieved successfully.");
            return Ok(roomType);
        }
        
        // POST: api/RoomTypes
        [HttpPost]
        public async Task<ActionResult<RoomTypeDto>> Create(RoomTypeDto roomTypeDto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var createdRoomType = await _service.AddAsync(roomTypeDto);
            Console.WriteLine($"Operation type with Code = {roomTypeDto.RoomTypeCode} was created successfully.");
            return CreatedAtAction(nameof(GetByCode), new { code = createdRoomType.RoomTypeCode }, createdRoomType);
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
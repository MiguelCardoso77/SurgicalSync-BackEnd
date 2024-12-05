using System.Collections.Generic;
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
            var allRoomTypes = await _service.GetAllAsync();
            return Ok(allRoomTypes);
        }
        
        // GET: api/RoomTypes/RTC
        [HttpGet("{code}")]
        public async Task<ActionResult<RoomTypeDto>> GetByCode(string code)
        {
            var roomType = await _service.GetByCodeAsync(code);
            return Ok(roomType);
        }
        
        // POST: api/RoomTypes
        [HttpPost]
        public async Task<ActionResult<RoomTypeDto>> Create(RoomTypeDto roomTypeDto)
        {
            var createdRoomType = await _service.AddAsync(roomTypeDto);
            return CreatedAtAction(nameof(GetByCode), new { code = createdRoomType.RoomTypeCode }, createdRoomType);
        }
    }
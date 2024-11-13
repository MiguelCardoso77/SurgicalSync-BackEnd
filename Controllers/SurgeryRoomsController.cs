using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.SurgeryRooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeryRoomsController : ControllerBase
    {
        private readonly SurgeryRoomService _surgeryRoomService;

        public SurgeryRoomsController(SurgeryRoomService surgeryRoomService)
        {
            _surgeryRoomService = surgeryRoomService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurgeryRoomDto>>> GetAll()
        {
            return await _surgeryRoomService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SurgeryRoomDto>> GetById(string id)
        {
            var surgeryRoom = await _surgeryRoomService.GetByIdAsync(new RoomNumber(id));

            if (surgeryRoom == null)
            {
                return NotFound();
            }
            return surgeryRoom;
        }
        

        [HttpPost]
        public async Task<ActionResult<SurgeryRoomDto>> Create(SurgeryRoomDto surgeryRoomDto)
        {
            var surgeryRoom = await _surgeryRoomService.AddAsync(surgeryRoomDto);

            return CreatedAtAction(nameof(GetById), new { id = surgeryRoom.RoomNumber }, surgeryRoom);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<SurgeryRoomDto>> Update(string id, SurgeryRoomDto surgeryRoomDto)
        {
            if (id != surgeryRoomDto.RoomNumber)
            {
                return BadRequest();
            }

            try
            {
                var surgeryRoom = await _surgeryRoomService.UpdateAsync(surgeryRoomDto);

                if (surgeryRoom == null)
                {
                    return NotFound();
                }

                return surgeryRoom;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SurgeryRoomDto>> Delete(string id)
        {
            try
            {
                var surgeryRoom = await _surgeryRoomService.InactivateAsync(new RoomNumber(id));

                if (surgeryRoom == null)
                {
                    return NotFound();
                }

                return Ok(surgeryRoom);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
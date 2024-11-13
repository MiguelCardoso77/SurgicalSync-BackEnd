using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.SurgeryRooms;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeryRoomsController
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
        /**
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
            var surgeryRoom = await _surgeryRoomService.AddAsync(surgeryRoomDto)
        }
        */
    }
}
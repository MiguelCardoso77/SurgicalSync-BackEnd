using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.SurgeryRooms;
using DDDNetCore.Infraestructure.SurgeryRooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeryRoomsController : ControllerBase
    {
        private readonly SurgeryRoomService _surgeryRoomService;
        private readonly SurgeryRoomRepository _surgeryRoomRepository;

        public SurgeryRoomsController(SurgeryRoomService surgeryRoomService)
        {
            _surgeryRoomService = surgeryRoomService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurgeryRoomDto>>> GetAll()
        {
            if (!AuthorizeRequest())
            {
                return Unauthorized("Access Denied.");
            }

            return await _surgeryRoomService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurgeryRoomDto>> GetById(string id)
        {
            if (!AuthorizeRequest())
            {
                return Unauthorized("Access Denied.");
            }

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
            if (!AuthorizeRequest())
            {
                return Unauthorized("Access Denied.");
            }

            var surgeryRoom = await _surgeryRoomService.AddAsync(surgeryRoomDto);

            return CreatedAtAction(nameof(GetById), new { id = surgeryRoom.RoomNumber }, surgeryRoom);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SurgeryRoomDto>> Update(string id, SurgeryRoomDto surgeryRoomDto)
        {
            if (!AuthorizeRequest())
            {
                return Unauthorized("Access Denied.");
            }

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SurgeryRoomDto>> Delete(string id)
        {
            if (!AuthorizeRequest())
            {
                return Unauthorized("Access Denied.");
            }

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

        private bool AuthorizeRequest()
        {
            var authorizationHeader = Request.Headers.Authorization.FirstOrDefault();
            const string validAuthorizationToken = "ICcTTh51IzOiBKmftT1SnrBH5d42";

            if (string.IsNullOrEmpty(authorizationHeader) || authorizationHeader != validAuthorizationToken)
            {
                return false;
            }

            return true;

            // Endpoint para obter informações de uma sala de cirurgia
            [HttpGet("{id}")]
              ActionResult<SurgeryRoomDto> GetRoomInfo(RoomNumber id)
            {
                // Obtenha a sala de cirurgia do repositório
                var surgeryRoom = _surgeryRoomService.GetByIdAsync(id);

                if (surgeryRoom == null)
                {
                    return NotFound(new { message = "Sala de cirurgia não encontrada." });
                }

                return Ok(surgeryRoom);
            }
        }
    }
}
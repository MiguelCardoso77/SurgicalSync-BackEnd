using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
        {
            return await _appointmentService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById(String id)
        {
            var appointment = await _appointmentService.GetById(new AppointmentId(id));

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto appointmentDto)
        {
            var appointment = await _appointmentService.AddAsync(appointmentDto);
            
            return CreatedAtAction(nameof(GetById), new {id = appointment.Id}, appointment);
        }
        
        [HttpPost(template:"planning")]
        public async Task<ActionResult<PlanningDto>> CreatePlanning(PlanningDto planningDto)
        {
            var planning = await _appointmentService.AddPlanningAsync(planningDto);
            
            return CreatedAtAction(nameof(GetById), new {id = planning.RoomNumber}, planning);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDto>> Update(String id, AppointmentDto appointmentDto)
        {
            if (id != appointmentDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var appointment = await _appointmentService.UpdateAsync(appointmentDto);

                if (appointment == null)
                {
                    return NotFound();
                }

                return appointment;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AppointmentDto>> Delete(String id)
        {
            try
            {
                var appointment = await _appointmentService.InactivateAsync(new AppointmentId(id));

                if (appointment == null)
                {
                    return NotFound();
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
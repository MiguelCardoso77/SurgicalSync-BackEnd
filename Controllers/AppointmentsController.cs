using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            return await _appointmentService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById(String id)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var appointment = await _appointmentService.GetById(new AppointmentId(id));

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }
        
        [HttpGet(template:"availableMaterial")]
        public async Task<ActionResult<AvailableMaterialsDTO>> GetAvailableMaterials([FromQuery] string time = null)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var appointment = await _appointmentService.GetAvailableMaterials(time);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto appointmentDto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var appointment = await _appointmentService.AddAsync(appointmentDto);
            
            return CreatedAtAction(nameof(GetById), new {id = appointment.Id}, appointment);
        }
        
        [HttpPost(template:"load-planning")]
        public async Task<ActionResult<PlanningDto>> LoadPlanning(PlanningDto planningDto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var planning = await _appointmentService.LoadPlanningAsync(planningDto);
            
            Console.WriteLine("Planning data loaded.");
            return Ok(planning);
        }
        
        [HttpPost(template:"planning")]
        public async Task<ActionResult<PlanningDto>> CreatePlanning(PlanningDto planningDto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
            var planning = await _appointmentService.AddPlanningAsync(planningDto);
            
            Console.WriteLine("Planning created for room: " + planning.RoomNumber);
            return CreatedAtAction(nameof(GetById), new {id = planning.RoomNumber}, planning);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDto>> Update(String id, AppointmentDto appointmentDto)
        {
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
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
            if (!AuthorizeRequest()) { return Unauthorized("Access Denied."); }
            
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
}
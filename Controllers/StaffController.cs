using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _service;

        /**
         * Initializes a new instance of the StaffController class.
         *
         * @param service The StaffService to handle staff-related operations.
         */
        public StaffController(StaffService service)
        {
            _service = service;
        }


        // GET: api/staff/st1
        /**
         * Retrieves a staff member by their unique identifier.
         *
         * @param id The unique identifier of the staff member.
         * @return An ActionResult containing the StaffDto if found, or a NotFound result if not found.
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetById(string id)
        {
            var sT = await _service.GetByIdAsync(new StaffId(id));

            if (sT == null)
            {
                return NotFound();
            }

            return sT;
        }

        // GET: api/OperationTypes
        /**
        * Retrieves all staff members, optionally filtered by name, email, or specialization.
        *
        * @param staffName Optional staff name to filter the results.
        * @param staffEmail Optional staff email to filter the results.
        * @param StaffSpecialization Optional specialization to filter the results.
        * @return An ActionResult containing a list of StaffDto objects that match the filter criteria.
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAll([FromQuery] string staffName = null,
            [FromQuery] string userEmail = null, [FromQuery] string StaffSpecialization = null)
        {
            if (!string.IsNullOrEmpty(StaffSpecialization))
            {
                var result = await _service.GetAllBySpecialization(StaffSpecialization);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(staffName))
            {
                var result = await _service.GetAllByName(staffName);
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                var result = await _service.GetAllByEmail(userEmail);
                return Ok(result);
            }

            if (string.IsNullOrEmpty(StaffSpecialization) && string.IsNullOrEmpty(staffName) &&
                string.IsNullOrEmpty(userEmail))
            {
                var allStaff = await _service.GetAllAsync();
                Console.WriteLine("All staffs listed successfully!");
                return Ok(allStaff);
            }

            return null;
        }


        // POST: api/Staff
        /**
         * Adds a new staff member.
         *
         * @param dto The StaffDto containing the staff member's information.
         * @return An ActionResult containing the created StaffDto and a location header for the new resource.
         */
        [HttpPost]
        public async Task<ActionResult<StaffDto>> AddAsync(StaffDto dto)
        {
            var staff = await _service.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff);
        }

        // PUT: api/Staff/S5
        /**
         * Updates an existing staff member's information.
         *
         * @param id The unique identifier of the staff member to update.
         * @param dto The StaffDto containing the updated staff member's information.
         * @return An ActionResult containing the updated StaffDto if successful, or a NotFound result if the staff member does not exist.
         */
        [HttpPut("{id}")]
        public async Task<ActionResult<StaffDto>> Update(String id, StaffDto dto)
        {
            if (id != dto.Id)
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


        // DEACTIVATE: api/Staff/Deactivate/S6
        /**
        * Deactivates a staff member by their unique identifier.
        *
        * @param id The unique identifier of the staff member to deactivate.
        * @return An ActionResult containing the deactivated StaffDto if successful, or a NotFound result if the staff member does not exist.
        */
        [HttpDelete("{id}")]
        public async Task<ActionResult<StaffDto>> Deactivate(string id)
        {
            // Chamando o serviço para desativar o usuário, assumindo que a lógica de desativação seja implementada no serviço.
            var result = await _service.DeactivateAsync(new StaffId(id));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
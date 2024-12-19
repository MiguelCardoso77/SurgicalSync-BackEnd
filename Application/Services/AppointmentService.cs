using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.SurgeryRooms;
using DDDNetCore.Infraestructure;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Service for handling appointment-related operations.
     */
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentsRepository _repository;
        private readonly ILogger<AppointmentService> _logger;
        private readonly AppointmentMapper _mapper;
        private readonly PlanningBootstrap _planningBootstrap;
        
        private readonly StaffService _staffService;
        private readonly SurgeryRoomService _surgeryRoomService;
        
        /**
         * Initializes a new instance of the AppointmentService class.
         * @param unitOfWork The unit of work for database operations.
         * @param repository The repository for appointment data access.
         * @param logger The logger for logging information.
         * @param mapper The mapper for converting between domain and DTO objects.
         */
        public AppointmentService(IUnitOfWork unitOfWork, IAppointmentsRepository repository,
        ILogger<AppointmentService> logger, AppointmentMapper mapper, PlanningBootstrap planningBootstrap,
        StaffService staffService, SurgeryRoomService surgeryRoomService)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
            _planningBootstrap = planningBootstrap;
            _staffService = staffService;
            _surgeryRoomService = surgeryRoomService;
        }
        
        /**
        * Retrieves all appointments.
        * @return A list of all appointments as AppointmentDto.
        */
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            
            List<AppointmentDto> appointmentDto = list.ConvertAll<AppointmentDto>(a => _mapper.ToDto(a));

            return appointmentDto;
        }
        
        /**
         * Retrieves an appointment by its ID.
         * @param appointmentId The ID of the appointment to retrieve.
         * @return The AppointmentDto if found; otherwise, null.
         */
        public async Task<AppointmentDto> GetById(AppointmentId appointmentId)
        {
            var appointment = await _repository.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                return null;
            }

            var response = _mapper.ToDto(appointment);

            return response;
        }
        
        /**
         * Retrieves available materials for a given time.
         * @param time The time for which to retrieve available materials.
         * @return The AvailableMaterialsDTO containing available staff and rooms.
         */
        public async Task<AvailableMaterialsDTO> GetAvailableMaterials(string time)
        {
            var staff = await _staffService.GetAvailableStaff(time);
            var rooms = await _surgeryRoomService.GetAllAsync();
            
            var availableMaterials = new AvailableMaterialsDTO
            {
                Staff = string.Join(",", staff.Select(s => s.Id)),
                Rooms = string.Join(",", rooms.Select(r => r.RoomNumber))
            };
            
            return availableMaterials;
        }
        
        /**
        * Adds a new appointment.
        * @param appointmentDto The data transfer object containing appointment details.
        * @return The newly created AppointmentDto.
        */
        public async Task<AppointmentDto> AddAsync(AppointmentDto appointmentDto)
        { 
            var appointmentId = await GenerateAppointmentId(appointmentDto);
            
            var totalMinutes = 0;
            var apTime = appointmentDto.Time;
            
            if (int.TryParse(appointmentDto.Time, out var timeInMinutes))
            {
                totalMinutes = timeInMinutes;
            }
            else if (TimeSpan.TryParse(apTime, out var parsedTime))
            {
                totalMinutes = (int)parsedTime.TotalMinutes;
            }
            
            appointmentDto.Time = totalMinutes.ToString();
            
            var domainObj = _mapper.ToDomain(appointmentDto, appointmentId);
            
            await this._repository.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();
            
            _logger.LogInformation("Appointment with Id: {AppointmentId} was successfully added!", domainObj.Id.AsString());
            
            return _mapper.ToDto(domainObj);
        }
        
        /**
         * Updates an existing appointment.
         * @param appointmentDto The data transfer object containing updated appointment details.
         * @return The updated AppointmentDto if successful; otherwise, null.
         */
        public async Task<AppointmentDto> UpdateAsync(AppointmentDto appointmentDto)
        {
            var appointment = await _repository.GetByIdAsync(new AppointmentId(appointmentDto.Id));

            if (appointment == null)
            {
                return null;
            }
            
            appointment.ChangeDate(new Date(DateTime.Parse(appointmentDto.Date)));
            
            appointment.ChangeStatus(Enum.Parse<Status>(appointmentDto.Status));
            
            if (!int.TryParse(appointmentDto.Time, out var timeInMinutes))
            {
                timeInMinutes = 1;
                Console.WriteLine("Error: Unable to parse appointment time. Defaulting to 1 minute.");
            }
            
            appointment.ChangeTime(new Time(timeInMinutes));
            
            appointment.ChangeRoomNumber(new RoomNumber(appointmentDto.RoomNumber));
            
            appointment.ChangeRequiredStaff(new RequiredStaff(appointmentDto.RequiredStaff));
            
            await _unitOfWork.CommitAsync();
            
            return _mapper.ToDto(appointment);
        }
        
        /**
         * Inactivates (deletes) an appointment by its ID.
         * @param appointmentId The ID of the appointment to inactivate.
         * @return The inactivated AppointmentDto if successful; otherwise, null.
         */
        public async Task<AppointmentDto> InactivateAsync(AppointmentId appointmentId)
        {
            var appointment = await _repository.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                return null;
            }
            
            _repository.Remove(appointment);
            await _unitOfWork.CommitAsync();
            
            return _mapper.ToDto(appointment);
        }
        
        /**
         * Generates a unique ID for a new appointment.
         * @param appointmentDto The data transfer object containing appointment details.
         * @param appointments The list of existing appointments to check for ID uniqueness.
         * @return A unique AppointmentId.
         */
        public async Task<AppointmentId> GenerateAppointmentId(AppointmentDto appointmentDto)
        {
            var appointments = await this._repository.GetAllAsync();

            if (appointments.Count > 0)
            {
                var lastAppointment = appointments.Last();

                var lastAppointmentId = lastAppointment.Id.AsString();
                
                int newRoomNumber = int.Parse(lastAppointmentId) + 1;
                return new AppointmentId(newRoomNumber.ToString());
            }
            else
            {
                return new AppointmentId("1");
            }
        }
        
        public async Task<PlanningDto> LoadPlanningAsync(PlanningDto planningDto)
        {
            await _planningBootstrap.BootstrapData(planningDto.Date, planningDto.OperationRequests);
            
            return planningDto;
        }
        
        public async Task<PlanningDto> AddPlanningAsync(PlanningDto planningDto)
        {
            var room = "sR" + planningDto.RoomNumber;
            var date = DateTime.Parse(planningDto.Date).ToString("yyyyMMdd");
            
            var requestUri = $"http://localhost:8888/best?room={room}&day={date}";
            Console.WriteLine(requestUri);
            
            using (var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8888") })
            {
                var response = await httpClient.GetAsync(requestUri);
                var responseContent = await response.Content.ReadAsStringAsync();
        
                Console.WriteLine("Best time slot response: " + responseContent);
            }
            
            return planningDto;
        }
    }
}
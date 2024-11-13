using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.SurgeryRooms;
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
        /**
         * Initializes a new instance of the AppointmentService class.
         * @param unitOfWork The unit of work for database operations.
         * @param repository The repository for appointment data access.
         * @param logger The logger for logging information.
         * @param mapper The mapper for converting between domain and DTO objects.
         */
        public AppointmentService(IUnitOfWork unitOfWork, IAppointmentsRepository repository,
            ILogger<AppointmentService> logger, AppointmentMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
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
        * Adds a new appointment.
        * @param appointmentDto The data transfer object containing appointment details.
        * @return The newly created AppointmentDto.
        */
        public async Task<AppointmentDto> addAsync(AppointmentDto appointmentDto)
        {
            var list = await this._repository.GetAllAsync();
            
            var existingAppointment = list.FirstOrDefault(a => a.RoomNumber.AsString() == appointmentDto.RoomNumber);
            
            var appointmentId = GenerateAppointmentId(appointmentDto, list);
            
            var domainObj = _mapper.ToDomain(appointmentDto, appointmentId);
            
            await this._repository.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();
            
            _logger.LogInformation("Appointment with Id: {AppointmentId} was successfully added!", domainObj.Id.AsString());
            
            var dto = _mapper.ToDto(domainObj);
            return dto;
        }
        /**
         * Updates an existing appointment.
         * @param appointmentDto The data transfer object containing updated appointment details.
         * @return The updated AppointmentDto if successful; otherwise, null.
         */
        public async Task<AppointmentDto> updateAsync(AppointmentDto appointmentDto)
        {
            var appointment = await _repository.GetByIdAsync(new AppointmentId(appointmentDto.Id));

            if (appointment == null)
            {
                return null;
            }
            
            appointment.ChangeDate(new Date(DateTime.Parse(appointmentDto.Date)));
            
            appointment.ChangeStatus(Enum.Parse<Status>(appointmentDto.Status));
            
            var timeInMinutes = int.Parse(appointmentDto.Time);
            
            appointment.ChangeTime(new Time(timeInMinutes));
            
            appointment.ChangeRoomNumber(new RoomNumber(appointmentDto.RoomNumber));
            
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
        public AppointmentId GenerateAppointmentId(AppointmentDto appointmentDto, List<Appointment> appointments)
        {
            bool exists = appointments.Any(a => a.RoomNumber.AsString() == appointmentDto.RoomNumber);

            if (exists)
            {
                return new AppointmentId(appointmentDto.RoomNumber);
            }

            if (appointments.Count > 0)
            {
                var lastAppointment = appointments.Last();
                
                var lastAppointmentId = lastAppointment.RoomNumber.AsString();
                
                int newRoomNumber = int.Parse(lastAppointmentId) + 1;
                return new AppointmentId(newRoomNumber.ToString());
            }
            else
            {
                return new AppointmentId("1");
            }
        }
    }
}
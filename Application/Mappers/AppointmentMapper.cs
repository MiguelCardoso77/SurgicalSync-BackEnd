using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.SurgeryRooms;

namespace DDDNetCore.Application.Mappers
{
    /**
     * Mapper class for converting between Appointment domain entities and AppointmentDto objects.
     * Provides methods to convert to and from DTO and domain model, as well as a method for 
     * converting lists of domain entities to DTOs.
     */
    public class AppointmentMapper
    {
        /**
         * Converts an Appointment domain entity to an AppointmentDto.
         *
         * @param appointment The Appointment domain entity to convert.
         * @return An AppointmentDto representing the domain entity.
         */
        public AppointmentDto ToDto(Appointment appointment)
        {
            return new AppointmentDto
            {
                Id = appointment.Id.AsString(),
                Status = appointment.Status.ToString(),
                Date = appointment.Date.DateTime.ToString("yyyyMMdd"),
                Time = appointment.Time.ToString(),
                RoomNumber = appointment.RoomNumber.AsString(),
            };
        }

        /**
         * Converts an AppointmentDto to an Appointment domain entity.
         *
         * @param dto The AppointmentDto to convert.
         * @param id The unique identifier for the Appointment.
         * @return An Appointment domain entity representing the DTO.
         * @throws FormatException If the date or time format in the DTO is invalid.
         */
        public Appointment ToDomain(AppointmentDto dto, AppointmentId id)
        {
            var parsedDate = DateTime.Parse(dto.Date);
            var timeInMinutes = int.Parse(dto.Time);
            return new Appointment(id, Enum.Parse<Status>(dto.Status), new Date(parsedDate), new Time(timeInMinutes), new RoomNumber(dto.RoomNumber));
        }

        /**
         * Converts a list of Appointment domain entities to a list of AppointmentDto objects.
         *
         * @param appointmentsList The list of Appointment domain entities to convert.
         * @return A list of AppointmentDto objects representing the domain entities.
         */
        public List<AppointmentDto> ToListDto(List<Appointment> appointmentsList)
        {
            return appointmentsList.Select(domain => ToDto(domain)).ToList();
        }
    }
}

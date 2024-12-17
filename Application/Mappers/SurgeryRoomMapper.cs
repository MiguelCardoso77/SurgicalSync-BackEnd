using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.RoomTypes;
using DDDNetCore.Domain.SurgeryRooms;
using Type = DDDNetCore.Domain.SurgeryRooms.Type;

namespace DDDNetCore.Application.Mappers
{
    /**
     * Provides mapping functionality between SurgeryRoom domain objects and SurgeryRoomDto data transfer objects.
     * This class includes methods for converting a SurgeryRoom object to a SurgeryRoomDto and vice versa.
     */
    public class SurgeryRoomMapper
    {
        /**
         * Converts a SurgeryRoom domain object to a SurgeryRoomDto data transfer object.
         *
         * @param surgeryRoom The SurgeryRoom domain object to convert.
         * @return A SurgeryRoomDto object containing the data from the specified SurgeryRoom.
         */
        public SurgeryRoomDto ToDto(SurgeryRoom surgeryRoom)
        {
            return new SurgeryRoomDto
            {
                RoomNumber = surgeryRoom.Id.AsString(),
                MaintenanceSlots = surgeryRoom.MaintenanceSlots.ToString(),
                CurrentStatus = surgeryRoom.CurrentStatus.ToString(),
                AssignedEquipment = surgeryRoom.AssignedEquipment.ToString(),
                Capacity = surgeryRoom.Capacity.ToString(),
                Type = surgeryRoom.Type.ToString()
            };
        }

        /**
         * Converts a SurgeryRoomDto data transfer object to a SurgeryRoom domain object.
         *
         * @param surgeryRoomDto The SurgeryRoomDto object containing the data to map.
         * @param roomNumber The RoomNumber to associate with the new SurgeryRoom domain object.
         * @return A SurgeryRoom object created from the data in the SurgeryRoomDto.
         */
        public SurgeryRoom FromDto(SurgeryRoomDto surgeryRoomDto, RoomNumber roomNumber)
        {
            return new SurgeryRoom(roomNumber, new MaintenanceSlots(surgeryRoomDto.MaintenanceSlots),
                Enum.Parse<CurrentStatus>(surgeryRoomDto.CurrentStatus),
                new AssignedEquipment(surgeryRoomDto.AssignedEquipment), new Capacity(surgeryRoomDto.Capacity),
                new RoomTypeId(surgeryRoomDto.Type));
        }

        /**
         * Converts a list of domain SurgeryRoom objects to a list of SurgeryRoomDto objects.
         * @param domainList a list of SurgeryRoom domain objects to be converted.
         * @return a list of SurgeryRoomDto objects corresponding to the provided domain objects.
        */

        public List<SurgeryRoomDto> ToDtoList(List<SurgeryRoom> surgeryRooms)
        {
            return surgeryRooms.Select(domain => ToDto(domain)).ToList();
        }
    }
}

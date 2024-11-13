namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) representing a surgery room.
     * This DTO is used to transfer surgery room data between
     * application layers.
     */
    public class SurgeryRoomDto
    {
        /**
         * The room number of the surgery room.
         */
        public string RoomNumber { get; set; }

        /**
         * The maintenance slots available for the surgery room.
         */
        public string MaintenanceSlots { get; set; }

        /**
         * The current status of the surgery room.
         */
        public string CurrentStatus { get; set; }

        /**
         * The equipment assigned to the surgery room.
         */
        public string AssignedEquipment { get; set; }

        /**
         * The capacity of the surgery room.
         */
        public string Capacity { get; set; }

        /**
         * The type of the surgery room.
         */
        public string Type { get; set; }
    }
}
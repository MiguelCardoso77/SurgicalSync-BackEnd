using DDDNetCore.Domain.RoomTypes;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.SurgeryRooms;

namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Represents a surgery room in a healthcare facility.
     * The surgery room is associated with a unique room number and contains various details
     * such as maintenance slots, current status, assigned equipment, capacity, and type.
     */
    public class SurgeryRoom : Entity<RoomNumber>, IAggregateRoot
    {
        /**
         * The unique identifier for this surgery room (room number).
         */
        public new RoomNumber Id { get; private set; }
        
        /**
         * The maintenance slots available for this surgery room.
         */
        public MaintenanceSlots MaintenanceSlots { get; private set; }
        
        /**
         * The current status of the surgery room (e.g., available, occupied, under maintenance).
         */
        public CurrentStatus CurrentStatus { get; private set; }
        
        /**
         * The equipment assigned to this surgery room.
         */
        public AssignedEquipment AssignedEquipment { get; private set; }
        
        /**
         * The capacity of this surgery room (e.g., number of patients it can accommodate).
         */
        public Capacity Capacity { get; private set; }
        
        /**
         * The type of this surgery room.
         */
        public RoomTypeId Type { get; private set; }

        /**
         * Private constructor for ORM or serialization frameworks.
         */
        private SurgeryRoom() {}

        /**
         * Initializes a new instance of the SurgeryRoom class with specified properties.
         *
         * @param roomNumber The unique identifier for the room (room number).
         * @param maintenanceSlots The available maintenance slots for the room.
         * @param currentStatus The current status of the room.
         * @param assignedEquipment The equipment assigned to the room.
         * @param capacity The capacity of the room.
         * @param type The type of surgery room.
         * 
         * @throws ArgumentException if any of the parameters are invalid.
         */
        public SurgeryRoom(RoomNumber roomNumber, MaintenanceSlots maintenanceSlots,
            CurrentStatus currentStatus, AssignedEquipment assignedEquipment, Capacity capacity,
            RoomTypeId type)
        {
            this.Id = roomNumber;
            this.MaintenanceSlots = maintenanceSlots;
            this.CurrentStatus = currentStatus;
            this.AssignedEquipment = assignedEquipment;
            this.Capacity = capacity;
            this.Type = type;
        }

        /**
         * Changes the maintenance slots of the surgery room.
         *
         * @param newMaintenanceSlots The new maintenance slots to set.
         */
        public void ChangeMaintenanceSlots(MaintenanceSlots newMaintenanceSlots)
        {
            this.MaintenanceSlots = newMaintenanceSlots;
        }

        /**
         * Changes the current status of the surgery room.
         *
         * @param newCurrentStatus The new current status to set (e.g., available, under maintenance).
         */
        public void ChangeCurrentStatus(CurrentStatus newCurrentStatus)
        {
            this.CurrentStatus = newCurrentStatus;
        }
    }
}

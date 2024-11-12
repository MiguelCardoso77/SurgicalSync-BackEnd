using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Represents a value object for maintenance slots, typically used to define time slots for maintenance activities.
     * Implements IValueObject with a string value.
     */
    public class MaintenanceSlots : IValueObject<string>
    {
        /**
         * The value representing the maintenance slot.
         * This property stores the string representation of the maintenance slot.
         */
        public string Value { get; private set; }
        /**
         * Private constructor for the MaintenanceSlots class.
         * This constructor is used for internal purposes and is not exposed for object creation.
         */
        private MaintenanceSlots()
        {
            
        }
        /**
         * Initializes a new instance of the MaintenanceSlots class with a specified maintenance slot value.
         *
         * @param maintenanceSlot The string value representing the maintenance slot.
         */
        public MaintenanceSlots(string maintenanceSlot)
        {
            this.Value = maintenanceSlot;
        }
        /**
         * Returns the string representation of the maintenance slot.
         *
         * @return The string value representing the maintenance slot.
         */
        public override string ToString()
        {
            return Value;
        }
        /**
        * Compares the current MaintenanceSlots object to another object for equality.
        * Two MaintenanceSlots objects are considered equal if their Value property is the same.
        *
        * @param obj The object to compare with.
        * @return True if the objects are equal, otherwise false.
        */
        public override bool Equals(object obj)
        {
            if (obj is MaintenanceSlots other)
            {
                return Value == other.Value;
            }

            return false;
        }
        /**
         * Returns a hash code for the MaintenanceSlots object based on its value.
         *
         * @return The hash code of the maintenance slot value.
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
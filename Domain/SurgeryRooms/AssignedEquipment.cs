using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Represents a value object for assigned equipment, typically used to define equipment associated with a surgery room.
     * Implements IValueObject with a string value.
     */
    public class AssignedEquipment : IValueObject<string>
    {
        /**
         * The value representing the assigned equipment.
         * This property stores the string representation of the equipment.
         */
        public string Value { get; private set; }
        /**
         * Private constructor for the AssignedEquipment class.
         * This constructor is used for internal purposes and is not exposed for object creation.
         */
        private AssignedEquipment()
        {
            
        }
        /**
       * Constructor that initializes the assigned equipment.
       *
       * @param value The value representing the room's assigned equipment.
       */
        public AssignedEquipment(string value)
        {
            this.Value = value;
        }
        
        /**
         * Returns the string representation of the assigned equipment.
         *
         * @return The string value representing the assigned equipment.
         */
        public override string ToString()
        {
            return Value;
        }
        /**
         * Compares the current AssignedEquipment object to another object for equality.
         * Two AssignedEquipment objects are considered equal if their Value property is the same.
         *
         * @param obj The object to compare with.
         * @return True if the objects are equal, otherwise false.
         */
        public override bool Equals(object obj)
        {
            if (obj is AssignedEquipment other)
            {
                return Value == other.Value;
            }

            return false;
        }
        /**
        * Returns a hash code for the AssignedEquipment object based on its value.
        *
        * @return The hash code of the assigned equipment value.
        */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
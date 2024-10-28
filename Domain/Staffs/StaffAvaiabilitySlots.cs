using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for staff availability slots.
     * This class encapsulates the available time slots for a staff member and ensures immutability.
     */
    public class StaffAvaiabilitySlots : IValueObject<string>
    {
        /**
         * The value representing the staff availability slots.
         * This could represent time ranges or dates when the staff member is available.
         */
        public string Value { get; private set; }


        private StaffAvaiabilitySlots()
        {
        }

        /**
         * Constructor that initializes the availability slots value.
         *
         * @param staffAvaiabilitySlots The value representing the staff's availability slots.
         */
        public StaffAvaiabilitySlots(string staffAvaiabilitySlots)
        {
            this.Value = staffAvaiabilitySlots;
        }

        /**
         * Converts the availability slots value to its string representation.
         *
         * @return A string representing the availability slots.
         */
        public override string ToString()
        {
            return Value;
        }

        /**
         * Checks equality between this instance and another object.
         *
         * @param obj The object to compare with this instance.
         * @return true if the other object is a StaffAvaiabilitySlots and the values are the same; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is StaffAvaiabilitySlots other)
            {
                return Value == other.Value;
            }

            return false;
        }

        /**
         * Gets the hash code for the availability slots value.
         *
         * @return The hash code for the availability slots value.
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
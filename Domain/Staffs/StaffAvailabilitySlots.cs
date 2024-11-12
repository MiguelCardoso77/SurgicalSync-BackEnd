using System.Linq;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for staff availability slots.
     * This class encapsulates the available time slots for a staff member and ensures immutability.
     */
    public class StaffAvailabilitySlots : IValueObject<string>
    {
        /**
         * The value representing the staff availability slots.
         * This could represent time ranges or dates when the staff member is available.
         */
        public string Value { get; private set; }


        private StaffAvailabilitySlots()
        {
        }

        /**
         * Constructor that initializes the availability slots value.
         *
         * @param staffAvailabilitySlots The value representing the staff's availability slots.
         */
        public StaffAvailabilitySlots(string staffAvailabilitySlots)
        {
            this.Value = staffAvailabilitySlots;
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
         * Converts the availability slots value into minutes.
         *
         * @param staffAvailabilitySlots The availability slots value to convert.
         * @return A string representing the availability slots in minutes.
         */
        public string ConvertIntoMinutes()
        {
            var times = Value.Split(',');
            var minutesList = times.Select(time =>
            {
                var parts = time.Split(':');
                var hours = int.Parse(parts[0]);
                var minutes = int.Parse(parts[1]);
                return (hours * 60 + minutes).ToString();
            });

            return string.Join(",", minutesList);
        }

        /**
         * Checks equality between this instance and another object.
         *
         * @param obj The object to compare with this instance.
         * @return true if the other object is a StaffAvailabilitySlots and the values are the same; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is StaffAvailabilitySlots other)
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
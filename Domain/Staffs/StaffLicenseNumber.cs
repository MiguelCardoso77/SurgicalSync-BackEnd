using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for a staff member's license number.
     * This class ensures immutability and value comparison for the license number.
     */
    public class StaffLicenseNumber : IValueObject
    {
        /**
         * The value of the staff member's license number.
         */
        public string StaffLicenseNumberValue { get; private set; }

        /**
         * Private constructor used for ORM or serialization purposes.
         * Initializes the object without setting any values.
         */
        private StaffLicenseNumber()
        {
        }

        /**
         * Constructor that initializes the staff license number with the provided value.
         *
         * @param staffLicenseNumber The license number to be assigned to the staff member.
         */
        public StaffLicenseNumber(string staffLicenseNumber)
        {
            this.StaffLicenseNumberValue = staffLicenseNumber;
        }

        /**
         * Converts the staff license number to its string representation.
         *
         * @return A string representing the staff license number.
         */
        public override string ToString()
        {
            return StaffLicenseNumberValue;
        }

        /**
         * Compares this StaffLicenseNumber object to another object for equality.
         *
         * @param obj The object to compare with this StaffLicenseNumber.
         * @return true if the other object is a StaffLicenseNumber and the license numbers are equal, false otherwise.
         */
        public override bool Equals(object obj)
        {
            if (obj is StaffLicenseNumber other)
            {
                return StaffLicenseNumberValue == other.StaffLicenseNumberValue;
            }

            return false;
        }

        /**
         * Gets the hash code of the staff license number value.
         *
         * @return The hash code of the staff license number value.
         */
        public override int GetHashCode()
        {
            return StaffLicenseNumberValue.GetHashCode();
        }
    }
}
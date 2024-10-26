using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for a staff member's phone number.
     * This class ensures immutability and value comparison for the staff phone number.
     */
    public class StaffPhoneNumber : IValueObject
    {
        /**
         * The value of the staff member's phone number.
         */
        public string StaffPhoneNumberValue { get; private set; }

        /**
         * Private constructor used for ORM or serialization purposes.
         * Initializes the object without setting any values.
         */
        private StaffPhoneNumber()
        {
        }

        /**
         * Constructor that initializes the staff phone number with the provided value.
         *
         * @param staffPhoneNumber The phone number to be assigned to the staff member.
         */
        public StaffPhoneNumber(string staffPhoneNumber)
        {
            this.StaffPhoneNumberValue = staffPhoneNumber;
        }

        /**
         * Converts the staff phone number to its string representation.
         *
         * @return A string representing the staff phone number.
         */
        public override string ToString()
        {
            return StaffPhoneNumberValue;
        }

        /**
         * Compares this StaffPhoneNumber object to another object for equality.
         *
         * @param obj The object to compare with this StaffPhoneNumber.
         * @return true if the other object is a StaffPhoneNumber and the phone number values are equal, false otherwise.
         */
        public override bool Equals(object obj)
        {
            if (obj is StaffPhoneNumber other)
            {
                return StaffPhoneNumberValue == other.StaffPhoneNumberValue;
            }

            return false;
        }

        /**
         * Gets the hash code of the staff phone number value.
         *
         * @return The hash code of the staff phone number value.
         */
        public override int GetHashCode()
        {
            return StaffPhoneNumberValue.GetHashCode();
        }
    }
}
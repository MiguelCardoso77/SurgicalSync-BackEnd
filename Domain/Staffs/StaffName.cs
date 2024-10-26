using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for a staff member's name.
     * This class ensures immutability and value comparison for the staff name.
     */
    public class StaffName : IValueObject
    {
        /**
         * The value of the staff member's name.
         */
        public string StaffNameValue { get; private set; }

        /**
         * Private constructor used for ORM or serialization purposes.
         * Initializes the object without setting any values.
         */
        private StaffName()
        {
        }

        /**
         * Constructor that initializes the staff name with the provided value.
         *
         * @param staffName The name to be assigned to the staff member.
         */
        public StaffName(string staffName)
        {
            this.StaffNameValue = staffName;
        }

        /**
         * Converts the staff name to its string representation.
         *
         * @return A string representing the staff name.
         */
        public override string ToString()
        {
            return StaffNameValue;
        }

        /**
         * Compares this StaffName object to another object for equality.
         *
         * @param obj The object to compare with this StaffName.
         * @return true if the other object is a StaffName and the name values are equal, false otherwise.
         */
        public override bool Equals(object obj)
        {
            if (obj is StaffName other)
            {
                return StaffNameValue == other.StaffNameValue;
            }

            return false;
        }

        /**
         * Gets the hash code of the staff name value.
         *
         * @return The hash code of the staff name value.
         */
        public override int GetHashCode()
        {
            return StaffNameValue.GetHashCode();
        }
    }
}
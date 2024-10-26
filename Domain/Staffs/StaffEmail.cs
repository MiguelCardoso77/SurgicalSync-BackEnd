using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for a staff member's email address.
     * This class ensures immutability and value comparison for the staff email.
     */
    public class StaffEmail : IValueObject
    {
        /**
         * The value of the staff member's email address.
         */
        public string StaffEmailValue { get; private set; }

        private StaffEmail()
        {
        }

        /**
         * Constructor that initializes the staff email with the given value.
         *
         * @param staffEmail The email address to be assigned to the staff member.
         */
        public StaffEmail(string staffEmail)
        {
            this.StaffEmailValue = staffEmail;
        }

        /**
         * Converts the staff email to its string representation.
         *
         * @return A string representing the staff email.
         */
        public override string ToString()
        {
            return StaffEmailValue;
        }

        /**
         * Compares this StaffEmail object to another object for equality.
         *
         * @param obj The object to compare with this StaffEmail.
         * @return true if the other object is a StaffEmail and the email values are equal, false otherwise.
         */
        public override bool Equals(object obj)
        {
            if (obj is StaffEmail other)
            {
                return StaffEmailValue == other.StaffEmailValue;
            }

            return false;
        }

        /**
         * Gets the hash code of the staff email value.
         *
         * @return The hash code of the staff email value.
         */
        public override int GetHashCode()
        {
            return StaffEmailValue.GetHashCode();
        }
    }
}
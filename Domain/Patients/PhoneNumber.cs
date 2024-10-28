using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a phone number of a patient in the system.
     *
     * The PhoneNumber class is a value object that encapsulates a patient's phone number,
     * ensuring that the phone number is treated as a single unit of value. It provides
     * methods for equality comparison and hashing based on the phone number value.
     */
    public class PhoneNumber : IValueObject<string>
    {
        public string Value { get; private set; }

        /**
         * Private constructor for the PhoneNumber class, used for ORM purposes.
         * Initializes the PhoneNumberValue to null.
         */
        private PhoneNumber()
        {
        }

        /**
         * Initializes a new instance of the PhoneNumber class with the specified phone number.
         *
         * @param phoneNumber The phone number of the patient.
         */
        public PhoneNumber(string phoneNumber)
        {
            this.Value = phoneNumber;
        }

        /**
         * Returns the string representation of the phone number.
         *
         * @return The phone number as a string.
         */
        public override string ToString()
        {
            return Value;
        }

        /**
         * Compares this PhoneNumber object with another object for equality.
         *
         * @param obj The object to compare with.
         * @return True if the specified object is a PhoneNumber and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber other)
            {
                return Value == other.Value;
            }

            return false;
        }

        /**
         * Returns a hash code for this PhoneNumber object.
         *
         * @return A hash code for the phone number.
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
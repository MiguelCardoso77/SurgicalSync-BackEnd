using System;
using System.Linq;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents the value object for a staff member's phone number.
     * This class ensures immutability and value comparison for the staff phone number.
     */
    public class StaffPhoneNumber : IValueObject<string>
    {
        /**
         * The value of the staff member's phone number.
         */
        public string Value { get; private set; }

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
         * @throws ArgumentException if the phone number is not exactly 9 numeric digits.
         */
        public StaffPhoneNumber(string staffPhoneNumber)
        {
            if (!IsValidPhoneNumber(staffPhoneNumber))
            {
                throw new ArgumentException("The phone number must contain exactly 9 numeric digits.");
            }

            this.Value = staffPhoneNumber;
        }

        /**
         * Validates that the provided phone number contains exactly 9 numeric digits.
         *
         * @param phoneNumber The phone number to validate.
         * @return true if the phone number is valid, false otherwise.
         */
        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) &&
                   phoneNumber.Length == 9 &&
                   phoneNumber.All(char.IsDigit); // Verifica se todos os caracteres são dígitos numéricos
        }

        /**
         * Converts the staff phone number to its string representation.
         *
         * @return A string representing the staff phone number.
         */
        public override string ToString()
        {
            return Value;
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
                return Value == other.Value;
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
            return Value.GetHashCode();
        }
    }
}

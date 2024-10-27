using System;
using System.Text.RegularExpressions;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    /**
     * Represents a unique identifier for a staff member.
     * The StaffId follows a specific format '(N | D | O)yyyynnnnn', where:
     * - 'N', 'D', or 'O' is a letter indicating a type/category.
     * - 'yyyy' is a 4-digit year.
     * - 'nnnnn' is a 5-digit number.
     */
    public class StaffId : EntityId
    {
        /**
         * Defines the format of the StaffId using a regular expression.
         * The format must start with either 'N', 'D', or 'O', followed by 9 digits.
         */
        private static readonly Regex StaffIdFormat = new Regex(@"^(N|D|O)\d{4}\d{5}$");

        /**
         * Constructor that accepts the value of the StaffId and validates its format.
         *
         * @param value The value of the StaffId to be validated and assigned.
         * @throws ArgumentException if the format is invalid.
         */
        public StaffId(string value) : base(value)
        {
            if (!IsValidFormat(value))
            {
                throw new ArgumentException(
                    "Invalid staff Id format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }
        }

        /**
         * Method to verify if the provided value matches the required StaffId format.
         *
         * @param value The value to be checked against the format.
         * @return true if the value matches the format, false otherwise.
         */
        private static bool IsValidFormat(string value)
        {
            return StaffIdFormat.IsMatch(value);
        }

        /**
         * Creates a StaffId from a string, ensuring the format is validated.
         *
         * @param text The text to be converted into a StaffId.
         * @return The validated StaffId.
         * @throws ArgumentException if the text does not match the required format.
         */
        protected override object createFromString(string text)
        {
            if (!IsValidFormat(text))
            {
                throw new ArgumentException(
                    "Invalid staff Id format. It must follow the format '(N | D | O)yyyynnnnn'.");
            }

            return text;
        }

        /**
         * Returns the StaffId as a string.
         *
         * @return The string representation of the StaffId.
         */
        public override string AsString()
        {
            return Value;
        }

        /**
         * Overrides the ToString method to return the StaffId as a string.
         *
         * @return The string representation of the StaffId.
         */
        public override string ToString()
        {
            return Value;
        }
    }
}
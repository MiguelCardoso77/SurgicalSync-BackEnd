using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a value object that encapsulates a patient's birth date.
     * This class holds the birth date as a string value.
     */
    public class BirthDate : IValueObject
    {
        public string BirthDateValue { get; private set; }

        private BirthDate()
        {
        }

        /**
         * Initializes a new instance of the BirthDate class with the specified birth date value.
         *
         * @param birthDate The birth date as a string.
         */
        public BirthDate(string birthDate)
        {
            this.BirthDateValue = birthDate;
        }

        /**
         * Returns a string representation of the birth date.
         *
         * @return The birth date as a string.
         */
        public override string ToString()
        {
            return BirthDateValue;
        }

        /**
         * Determines whether the specified object is equal to the current BirthDate object.
         *
         * @param obj The object to compare with the current BirthDate.
         * @return true if the specified object is a BirthDate and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is BirthDate other)
            {
                return BirthDateValue == other.BirthDateValue;
            }

            return false;
        }

        /**
         * Returns a hash code for this BirthDate object.
         *
         * @return A hash code for the current BirthDate.
         */
        public override int GetHashCode()
        {
            return BirthDateValue.GetHashCode();
        }
    }
}
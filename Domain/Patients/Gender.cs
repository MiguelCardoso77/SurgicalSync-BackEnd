using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class Gender : IValueObject
    {
        /**
     * Represents a value object that encapsulates a patient's gender information.
     * This class holds the gender as a string value.
     */
        public string GenderValue { get; private set; }

        private Gender()
        {
        }

        /**
         * Initializes a new instance of the Gender class with the specified gender value.
         *
         * @param gender The gender as a string.
         */
        public Gender(string gender)
        {
            this.GenderValue = gender;
        }

        /**
         * Returns a string representation of the gender.
         *
         * @return The gender as a string.
         */
        public override string ToString()
        {
            return GenderValue;
        }

        /**
         * Determines whether the specified object is equal to the current Gender object.
         *
         * @param obj The object to compare with the current Gender.
         * @return true if the specified object is a Gender and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is Gender other)
            {
                return GenderValue == other.GenderValue;
            }

            return false;
        }

        /**
         * Returns a hash code for this Gender object.
         *
         * @return A hash code for the current Gender.
         */
        public override int GetHashCode()
        {
            return GenderValue.GetHashCode();
        }
    }
}
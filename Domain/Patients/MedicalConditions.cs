using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a value object for medical conditions associated with a patient.
     * This class encapsulates the medical conditions as a string value.
     */
    public class MedicalConditions : IValueObject<string>
    {
        /**
         * Gets the string representation of the medical conditions.
         */
        public string Value { get; private set; }

        private MedicalConditions()
        {
        }

        /**
         * Initializes a new instance of the MedicalConditions class with the specified medical conditions.
         *
         * @param medicalConditions The medical conditions associated with a patient.
         */
        public MedicalConditions(string medicalConditions)
        {
            this.Value = medicalConditions;
        }

        /**
         * Returns a string representation of the medical conditions.
         *
         * @return A string that represents the medical conditions.
         */
        public override string ToString()
        {
            return Value;
        }

        /**
         * Determines whether the specified object is equal to the current object.
         *
         * @param obj The object to compare with the current object.
         * @return true if the specified object is equal to the current object; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is MedicalConditions other)
            {
                return Value == other.Value;
            }

            return false;
        }

        /**
         * Serves as a hash function for the current object.
         *
         * @return A hash code for the current object.
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
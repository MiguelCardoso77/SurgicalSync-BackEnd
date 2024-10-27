using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents the name of a patient in the system.
     *
     * The PatientName class is a value object that encapsulates a patient's name,
     * ensuring that the name is treated as a single unit of value. It provides
     * methods for equality comparison and hashing based on the name value.
     */
    public class PatientName : IValueObject
    {
        public string PatientNameValue { get; private set; }

        /**
         * Private constructor for the PatientName class, used for ORM purposes.
         * Initializes the PatientNameValue to null.
         */
        private PatientName()
        {
        }

        /**
         * Initializes a new instance of the PatientName class with the specified name.
         *
         * @param patientName The name of the patient.
         */
        public PatientName(string patientName)
        {
            this.PatientNameValue = patientName;
        }

        /**
         * Returns the string representation of the patient's name.
         *
         * @return The patient's name as a string.
         */
        public override string ToString()
        {
            return PatientNameValue;
        }

        /**
         * Compares this PatientName object with another object for equality.
         *
         * @param obj The object to compare with.
         * @return True if the specified object is a PatientName and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is PatientName other)
            {
                return PatientNameValue == other.PatientNameValue;
            }

            return false;
        }

        /**
         * Returns a hash code for this PatientName object.
         *
         * @return A hash code for the patient's name.
         */
        public override int GetHashCode()
        {
            return PatientNameValue.GetHashCode();
        }
    }
}
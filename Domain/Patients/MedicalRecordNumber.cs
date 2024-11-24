using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a unique medical record number for a patient, derived from the current year,
     * month, and a sequential number.
     *
     * This class inherits from EntityId, providing a unique identifier for patient records.
     */
    public class MedicalRecordNumber : EntityId
    {

        /**
         * Initializes a new instance of the MedicalRecordNumber class with a specified value.
         *
         * @param value The medical record number as a string.
         */
        public MedicalRecordNumber(string value) : base(value)
        {
        }
        
        /**
         * Creates an instance of MedicalRecordNumber from the specified string representation.
         *
         * @param text The string representation of the medical record number.
         * @return The medical record number as an object.
         */
        protected override Object createFromString(String text)
        {
            return text;
        }

        /**
         * Returns the string representation of the medical record number.
         *
         * @return The medical record number as a string.
         */
        public override string AsString()
        {
            return Value;
        }

        /**
         * Returns a string that represents the current medical record number.
         *
         * @return The medical record number as a string.
         */
        public override string ToString()
        {
            return Value;
        }
    }
}
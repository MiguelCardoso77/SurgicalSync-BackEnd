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
        private static int _sequentialNumber;

        /**
         * Initializes a new instance of the MedicalRecordNumber class with a generated
         * medical record number based on the current date and a sequential number.
         */
        public MedicalRecordNumber() : base(GenerateMedicalRecordNumber())
        {
        }

        /**
         * Initializes a new instance of the MedicalRecordNumber class with a specified value.
         *
         * @param value The medical record number as a string.
         */
        public MedicalRecordNumber(string value) : base(value)
        {
        }

        /**
         * Generates a unique medical record number based on the current year, month,
         * and a sequential number that increments with each new instance.
         *
         * @return A string representing the generated medical record number.
         */
        private static string GenerateMedicalRecordNumber()
        {
            // Get the current year and month
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");

            _sequentialNumber++;
            string seqNumber = _sequentialNumber.ToString("D6");

            return $"{year}{month}{seqNumber}";
        }

        /**
         * Creates an instance of MedicalRecordNumber from the specified string representation.
         *
         * @param text The string representation of the medical record number.
         * @return The medical record number as an object.
         */
        override
            protected Object createFromString(String text)
        {
            return text;
        }

        /**
         * Returns the string representation of the medical record number.
         *
         * @return The medical record number as a string.
         */
        override
            public string AsString()
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
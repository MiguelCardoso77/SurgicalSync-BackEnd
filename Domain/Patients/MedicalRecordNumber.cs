using System;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class MedicalRecordNumber : EntityId
    {
        private static int _sequentialNumber; 

        public MedicalRecordNumber() : base(GenerateMedicalRecordNumber())
        {
        }

        public MedicalRecordNumber(string value) : base(value)
        {
        }

        private static string GenerateMedicalRecordNumber()
        {
            // Get the current year and month
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");

            _sequentialNumber++;
            string seqNumber = _sequentialNumber.ToString("D6");

            return $"{year}{month}{seqNumber}";
        }

        override
            protected Object createFromString(String text)
        {
            return text;
        }

        override
            public string AsString()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
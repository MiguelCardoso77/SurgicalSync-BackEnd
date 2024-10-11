using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class PatientName : IValueObject
    {
        public string PatientNameValue { get; private set; }
        
        private PatientName(){ }

        public PatientName(string patientName)
        {
            this.PatientNameValue = patientName;
        }

        public override string ToString()
        {
            return PatientNameValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is PatientName other)
            {
                return PatientNameValue == other.PatientNameValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return PatientNameValue.GetHashCode();
        }
    }
}
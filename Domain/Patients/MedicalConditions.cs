using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class MedicalConditions : IValueObject
    {
        public string MedicalConditionsValue { get; private set; }
        
        private MedicalConditions(){ }

        public MedicalConditions(string medicalConditions)
        {
            this.MedicalConditionsValue = medicalConditions;
        }

        public override string ToString()
        {
            return MedicalConditionsValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is MedicalConditions other)
            {
                return MedicalConditionsValue == other.MedicalConditionsValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return MedicalConditionsValue.GetHashCode();
        }
    }
    
}
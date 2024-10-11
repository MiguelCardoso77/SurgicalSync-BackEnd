using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class BirthDate: IValueObject
    {
        public string BirthDateValue { get; private set; }
        
        private BirthDate(){ }

        public BirthDate(string birthDate)
        {
            this.BirthDateValue = birthDate;
        }

        public override string ToString()
        {
            return BirthDateValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is BirthDate other)
            {
                return BirthDateValue == other.BirthDateValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return BirthDateValue.GetHashCode();
        }
    }
}
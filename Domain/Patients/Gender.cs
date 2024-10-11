using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class Gender: IValueObject
    {
        public string GenderValue { get; private set; }
        
        private Gender(){ }

        public Gender(string gender)
        {
            this.GenderValue = gender;
        }

        public override string ToString()
        {
            return GenderValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is Gender other)
            {
                return GenderValue == other.GenderValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return GenderValue.GetHashCode();
        }
    }
}
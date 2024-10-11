using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class PhoneNumber: IValueObject
    {
        public string PhoneNumberValue { get; private set; }
        
        private PhoneNumber(){ }

        public PhoneNumber(string phoneNumber)
        {
            this.PhoneNumberValue = phoneNumber;
        }

        public override string ToString()
        {
            return PhoneNumberValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber other)
            {
                return PhoneNumberValue == other.PhoneNumberValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return PhoneNumberValue.GetHashCode();
        }
    }
}
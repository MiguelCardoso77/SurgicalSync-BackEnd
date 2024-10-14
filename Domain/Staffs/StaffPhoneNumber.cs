using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class StaffPhoneNumber: IValueObject
    {
        public string StaffPhoneNumberValue { get; private set; }
        
        private StaffPhoneNumber(){ }

        public StaffPhoneNumber(string staffPhoneNumber)
        {
            this.StaffPhoneNumberValue = staffPhoneNumber;
        }

        public override string ToString()
        {
            return StaffPhoneNumberValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is StaffPhoneNumber other)
            {
                return StaffPhoneNumberValue == other.StaffPhoneNumberValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return StaffPhoneNumberValue.GetHashCode();
        }
    }
}
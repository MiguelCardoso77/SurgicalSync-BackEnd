using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class StaffLicenseNumber : IValueObject
    {
        public string StaffLicenseNumberValue { get; private set; }
        
        private StaffLicenseNumber(){ }

        public StaffLicenseNumber(string staffLicenseNumber)
        {
            this.StaffLicenseNumberValue = staffLicenseNumber;
        }

        public override string ToString()
        {
            return StaffLicenseNumberValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is StaffLicenseNumber other)
            {
                return StaffLicenseNumberValue == other.StaffLicenseNumberValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return StaffLicenseNumberValue.GetHashCode();
        }
    }
}
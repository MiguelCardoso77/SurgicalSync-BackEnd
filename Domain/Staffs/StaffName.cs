using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class StaffName : IValueObject
    {
        public string StaffNameValue { get; private set; }
        
        private StaffName(){ }

        public StaffName(string staffName)
        {
            this.StaffNameValue = staffName;
        }

        public override string ToString()
        {
            return StaffNameValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is StaffName other)
            {
                return StaffNameValue == other.StaffNameValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return StaffNameValue.GetHashCode();
        }
    }
}
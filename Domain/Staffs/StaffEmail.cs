using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class StaffEmail : IValueObject
    {
        public string StaffEmailValue { get; private set; }
        
        private StaffEmail(){ }

        public StaffEmail(string staffEmail)
        {
            this.StaffEmailValue = staffEmail;
        }

        public override string ToString()
        {
            return StaffEmailValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is StaffEmail other)
            {
                return StaffEmailValue == other.StaffEmailValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return StaffEmailValue.GetHashCode();
        }
    }
}
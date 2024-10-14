using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Staffs
{
    public class StaffAvaiabilitySlots : IValueObject
    {
        public string StaffAvaiabilitySlotsValue { get; private set; }
        
        private StaffAvaiabilitySlots(){ }

        public StaffAvaiabilitySlots(string staffAvaiabilitySlots)
        {
            this.StaffAvaiabilitySlotsValue = staffAvaiabilitySlots;
        }

        public override string ToString()
        {
            return StaffAvaiabilitySlotsValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is StaffAvaiabilitySlots other)
            {
                return StaffAvaiabilitySlotsValue == other.StaffAvaiabilitySlotsValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return StaffAvaiabilitySlotsValue.GetHashCode();
        }
    }
}
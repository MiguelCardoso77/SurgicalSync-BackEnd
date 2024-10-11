using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class EmergencyContact: IValueObject
    {
        public string EmergencyContactValue { get; private set; }
        
        private EmergencyContact(){ }

        public EmergencyContact(string emergencyContact)
        {
            this.EmergencyContactValue = emergencyContact;
        }

        public override string ToString()
        {
            return EmergencyContactValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is EmergencyContact other)
            {
                return EmergencyContactValue == other.EmergencyContactValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return EmergencyContactValue.GetHashCode();
        }
    }
}
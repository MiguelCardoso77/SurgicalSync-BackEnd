using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a value object that encapsulates a patient's emergency contact information.
     * This class holds the emergency contact as a string value.
     */
    public class EmergencyContact : IValueObject<string>
    {
        public string Value { get; private set; }

        private EmergencyContact()
        {
        }

        /**
         * Initializes a new instance of the EmergencyContact class with the specified emergency contact value.
         *
         * @param emergencyContact The emergency contact as a string.
         */
        public EmergencyContact(string emergencyContact)
        {
            this.Value = emergencyContact;
        }

        /**
         * Returns a string representation of the emergency contact.
         *
         * @return The emergency contact as a string.
         */
        public override string ToString()
        {
            return Value;
        }

        /**
         * Determines whether the specified object is equal to the current EmergencyContact object.
         *
         * @param obj The object to compare with the current EmergencyContact.
         * @return true if the specified object is an EmergencyContact and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is EmergencyContact other)
            {
                return Value == other.Value;
            }

            return false;
        }

        /**
         * Returns a hash code for this EmergencyContact object.
         *
         * @return A hash code for the current EmergencyContact.
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a value object that holds the history of appointments for a patient.
     * This class encapsulates the appointment history as a string value.
     */
    public class AppointmentHistory : IValueObject
    {
        
        public string AppointmentHistoryValue { get; private set; }

        private AppointmentHistory()
        {
        }

        /**
         * Initializes a new instance of the AppointmentHistory class with the specified appointment history value.
         *
         * @param appointmentHistory The history of appointments as a string.
         */
        public AppointmentHistory(string appointmentHistory)
        {
            this.AppointmentHistoryValue = appointmentHistory;
        }

        /**
         * Returns a string representation of the appointment history.
         *
         * @return The appointment history as a string.
         */
        public override string ToString()
        {
            return AppointmentHistoryValue;
        }

        /**
         * Determines whether the specified object is equal to the current AppointmentHistory object.
         *
         * @param obj The object to compare with the current AppointmentHistory.
         * @return true if the specified object is an AppointmentHistory and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is AppointmentHistory other)
            {
                return AppointmentHistoryValue == other.AppointmentHistoryValue;
            }

            return false;
        }

        /**
         * Returns a hash code for this AppointmentHistory object.
         *
         * @return A hash code for the current AppointmentHistory.
         */
        public override int GetHashCode()
        {
            return AppointmentHistoryValue != null ? AppointmentHistoryValue.GetHashCode() : 0;
        }
    }
}
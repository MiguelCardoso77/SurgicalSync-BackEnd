using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class AppointmentHistory: IValueObject
    {
        public string AppointmentHistoryValue { get; private set; }
        
        private AppointmentHistory(){ }

        public AppointmentHistory(string appointmentHistory)
        {
            this.AppointmentHistoryValue = appointmentHistory;
        }

        public override string ToString()
        {
            return AppointmentHistoryValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is AppointmentHistory other)
            {
                return AppointmentHistoryValue == other.AppointmentHistoryValue;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return AppointmentHistoryValue != null ? AppointmentHistoryValue.GetHashCode() : 0;
        }
    }
}
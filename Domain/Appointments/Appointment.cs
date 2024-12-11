using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.SurgeryRooms;

namespace DDDNetCore.Domain.Appointments
{
    /**
     * Represents an appointment in the healthcare system.
     * An appointment is associated with a unique identifier, a status, a date, 
     * a time, and a room number for the scheduled procedure.
     */
    public class Appointment : Entity<AppointmentId>, IAggregateRoot
    {
        /**
         * The unique identifier for this appointment.
         */
        public new AppointmentId Id { get; protected set; }
        
        /**
         * The status of the appointment (e.g., scheduled, completed, cancelled).
         */
        public Status Status { get; protected set; }
        
        /**
         * The date of the appointment.
         */
        public Date Date { get; protected set; }
        
        /**
         * The time of the appointment.
         */
        public Time Time { get; protected set; }
        
        /**
         * The unique identifier for the operation request associated with this appointment.
         */
        public OperationRequestId OperationRequestId { get; protected set; }
        
        /**
         * The room number where the appointment will take place.
         */
        public RoomNumber RoomNumber { get; protected set; }
        
        /**
         * The required staff for the appointment.
         */
        public RequiredStaff RequiredStaff { get; protected set; }
        
        // Private constructor for EF
        private Appointment()
        {
         this.Id = null;
         this.Status = Status.Canceled;
         this.Date = null;
         this.Time = null;
         this.RoomNumber = null;
         this.OperationRequestId = null;
         this.RequiredStaff = null;
        }

        /**
         * Initializes a new instance of the Appointment class with specified properties.
         *
         * @param id The unique identifier for the appointment.
         * @param status The current status of the appointment.
         * @param date The date of the appointment.
         * @param time The time of the appointment.
         * @param roomNumber The room number assigned to the appointment.
         * 
         * @throws ArgumentException if any of the parameters are invalid.
         */
        public Appointment(AppointmentId id, Status status, Date date, Time time, RoomNumber roomNumber, 
        OperationRequestId request, RequiredStaff staff)
        {
            this.Id = id;
            this.Status = status;
            this.Date = date;
            this.Time = time;
            this.RoomNumber = roomNumber;
            this.OperationRequestId = request;
            this.RequiredStaff = staff;
        }

        /**
         * Changes the status of the appointment.
         *
         * @param status The new status to set for the appointment (e.g., scheduled, completed, cancelled).
         */
        public void ChangeStatus(Status status)
        {
            this.Status = status;
        }

        /**
         * Changes the date of the appointment.
         *
         * @param date The new date to set for the appointment.
         */
        public void ChangeDate(Date date)
        {
            this.Date = date;
        }

        /**
         * Changes the time of the appointment.
         *
         * @param time The new time to set for the appointment.
         */
        public void ChangeTime(Time time)
        {
            this.Time = time;
        }

        /**
         * Changes the room number assigned to the appointment.
         *
         * @param roomNumber The new room number to assign to the appointment.
         */
        public void ChangeRoomNumber(RoomNumber roomNumber)
        {
            this.RoomNumber = roomNumber;
        }

        /**
         * Changes the required staff for the appointment.
         *
         * @param staff The new required staff for the appointment.
         */
        public void ChangeRequiredStaff(RequiredStaff staff)
        {
            this.RequiredStaff = staff;
        }
    }
}

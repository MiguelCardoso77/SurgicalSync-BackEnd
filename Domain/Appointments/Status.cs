namespace DDDNetCore.Domain.Appointments
{
    /**
     * Enum representing the various statuses an appointment or resource can have.
     */
    public enum Status
    {
        /**
         * Indicates that the appointment is scheduled and awaiting fulfillment.
         */
        Scheduled,
        /**
        * Indicates that the appointment has been completed successfully.
        */
        Completed,
        /**
         * Indicates that the appointment has been canceled and will not take place.
         */
        Canceled
    }
}
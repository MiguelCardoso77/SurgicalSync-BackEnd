namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) representing an appointment.
     * This DTO is used to transfer appointment data between
     * application layers.
     */
    public class AppointmentDto
    {
        /**
         * The unique identifier of the appointment.
         */
        public string Id { get; set; }

        /**
         * The current status of the appointment.
         */
        public string Status { get; set; }

        /**
         * The date of the appointment.
         */
        public string Date { get; set; }

        /**
         * The time of the appointment.
         */
        public string Time { get; set; }

        /**
         * The room number where the appointment is scheduled.
         */
        public string RoomNumber { get; set; }
    }
}
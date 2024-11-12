namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Represents the different types of rooms available in a surgery facility.
     * These types categorize rooms based on their specific function within the medical environment.
     */
    public enum Type
    {
        /**
        * An Operating Room, typically used for performing surgeries.
        */
        OperatingRoom,
        /**
         * A Consultation Room, typically used for patient consultations and examinations.
         */
        ConsultationRoom,
        /**
         * An Intensive Care Unit (ICU), typically used for monitoring and treating critically ill patients.
         */
        ICU
    }
}
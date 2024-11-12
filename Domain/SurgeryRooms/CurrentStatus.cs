namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Enum representing the current status of a surgery room.
     */
    public enum CurrentStatus
    {
        /**
         * Indicates that the surgery room is available for use.
         */
        Available,
        /**
         * Indicates that the surgery room is currently occupied.
         */
        Occupied,
        /**
         * Indicates that the surgery room is under maintenance and not available for use.
         */
        UnderMaintenance
    }
}
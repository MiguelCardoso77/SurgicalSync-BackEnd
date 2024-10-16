namespace DDDNetCore.Domain.OperationRequests
{
    /**
     * Represents the priority levels for operation requests in the domain.
     * This enum defines three distinct priority levels to classify surgeries
     * based on their urgency, allowing for effective prioritization of medical procedures.
     */
    public enum Priority
    {
        /**
         * Low priority: Represents elective surgeries that can be scheduled at convenience.
         */
        ElectiveSurgery,
        /**
         * Medium priority: Represents urgent surgeries that require timely attention but are not life-threatening.
         */
        UrgentSurgery,
        /**
         * High priority: Represents emergency surgeries that must be performed immediately to save life or prevent serious harm.
         */
        EmergencySurgery
    }
}
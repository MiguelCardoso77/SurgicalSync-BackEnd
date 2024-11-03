namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) for an operation type.
     * Represents the data needed for communication between the application and external layers.
     */
    public class OperationTypeDto
    {
        public string Id { get; set; }
        public string OperationName { get; set; }
        public string RequiredStaff { get; set; }
        public string EstimatedDuration { get; set; }
    }
}
namespace DDDNetCore.Application.DTO
{
    /**
     * Data Transfer Object (DTO) for an operation request.
     * Represents the data needed for communication between the application and external layers.
     */
    public class OperationRequestDto
    {
        /**
         * Gets or sets the unique identifier for the operation request.
         */
        public string OperationRequestId { get; set; }
        /**
         * Gets or sets the deadline date for the operation request.
         * The date is represented as a string.
         */
        public string DeadlineDate { get; set; }
        /**
         * Gets or sets the priority level of the operation request.
         * Represented as a string.
         */
        public string Priority { get; set; }
        /**
         * Gets or sets the identifier for the type of operation.
         */
        public string OperationTypeId { get; set; }
        /**
         * Gets or sets the medical record number associated with the operation request.
         */
        public string MedicalRecordNumber { get; set; }
        /**
         * Gets or sets the license number of the medical professional related to the request.
         */
        public string StaffId { get; set; }
    }
}
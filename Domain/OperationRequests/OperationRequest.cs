using System;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationRequests {
    
    /**
     * The OperationRequest class represents a request for a medical operation.
     * It is an aggregate root in the domain model, responsible for handling
     * operation request properties such as priority, deadline, operation type, related patient
     * and doctor information.
     */
    public class OperationRequest : Entity<OperationRequestId>, IAggregateRoot
    {

        /**
         * The unique identifier for the operation request.
         */

        public new OperationRequestId Id { get; private set; }
      
        /**
         * The priority of the operation request (e.g., High, Medium, Low).
         */
        public Priority Priority { get; private set; }
        
        /**
         * The deadline date for when the operation needs to be performed.
         */
        
        public DeadlineDate DeadlineDate { get; private set; }
        
        /**
         * The identifier for the type of operation being requested.
         */
        
        public OperationTypeId OperationTypeId {get; private set; }
        
        /**
         * The patient's medical record number, linking the request to a specific patient.
         */
        public MedicalRecordNumber MedicalRecordNumber { get; private set; }
        
        /**
         * The license number of the doctor or medical professional responsible for the operation.
         */
        public StaffId StaffId { get; private set; }
        
        /**
         * Flag indicating if the operation request is active or inactive.
         */
        
        public bool IsActive { get; set; }
        
        /**
         * Default constructor for ORM and serialization purposes.
         */

        private OperationRequest()
        {
            this.DeadlineDate = null;
        }
        
        /**
         * Constructs a new OperationRequest with the given parameters.
         */

        public OperationRequest(OperationRequestId id, Priority priority, DeadlineDate deadlineDate,
            OperationTypeId operationTypeId, MedicalRecordNumber medicalRecordNumber, StaffId staffId)
        {
            this.Id = id ?? throw new ArgumentException(nameof(id), "OperationRequestId cannot be null.");
            this.Priority = priority;
            this.DeadlineDate = deadlineDate ?? throw new ArgumentException(nameof(deadlineDate), "DeadlineDate cannot be null.");
            this.OperationTypeId = operationTypeId ?? throw new ArgumentException(nameof(operationTypeId), "OperationTypeId cannot be null.");
            this.MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException(nameof(medicalRecordNumber), "MedicalRecordNumber cannot be null.");
            this.StaffId = staffId ?? throw new ArgumentException(nameof(staffId), "StaffId cannot be null.");
            this.IsActive  = true;
        }
        /**
         * Activates the operation request, marking it as active.
         */
        public void ActivateOperationRequest()
        {
            this.IsActive = true;
        }
        /**
         * Deactivates the operation request, marking it as inactive.
         */
        public void DeactivateOperationRequest()
        {
            this.IsActive = false;
        }
        /**
         * Changes the deadline date of the operation request to a new date.
         * <param name="newDeadlineDate"> The new deadline date to be set. It must be a valid DeadlineDate object.</param>
         */
        public void ChangeDeadlineDate(DeadlineDate newDeadlineDate)
        {
            this.DeadlineDate = newDeadlineDate;
        }
        /**
         * Changes the priority of the operation request to a new priority level.
         *  <param name="newPriority">The new priority to be set. It must be a valid Priority enum value.</param>
         */
        public void ChangePriority(Priority newPriority)
        {
            this.Priority = newPriority;
        }
    }
}
using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.OperationType
{
    /**
     * The OperationType class represents a type of medical operation that can be requested.
     * It is an aggregate root in the domain model, responsible for handling
     * operation type properties such as name, required staff, estimated duration,
     * and activation status.
     */
    public class OperationType : Entity<OperationTypeId>, IAggregateRoot
    {
        public new OperationTypeId Id { get; private set; }
        public OperationName Name { get; private set; }
        public List<RequiredStaff> RequiredStaff { get; private set; }
        public List<EstimatedDuration> EstimatedDuration { get; private set; }
        public bool IsActive { get; private set; }

        // Private constructor for EF
        private OperationType()
        {
            this.Name = null;
            this.RequiredStaff = null;
            this.EstimatedDuration = null;
            this.IsActive = false;
        }

        /**
         * Constructor that initializes the OperationType with a unique identifier, name, required staff, and estimated duration.
         * The operation type is activated by default.
         * @param id The unique identifier for the operation type.
         * @throws ArgumentNullException if any of the parameters are null.
         */
        public OperationType(OperationTypeId id, OperationName name, List<RequiredStaff> requiredStaff, List<EstimatedDuration> estimatedDuration)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id), "OperationTypeId cannot be null.");
            this.Name = name ?? throw new ArgumentNullException(nameof(name), "OperationName cannot be null.");
            this.RequiredStaff = requiredStaff ?? throw new ArgumentNullException(nameof(requiredStaff), "RequiredStaff cannot be null.");
            this.EstimatedDuration = estimatedDuration ?? throw new ArgumentNullException(nameof(estimatedDuration), "EstimatedDuration cannot be null.");
            this.IsActive = true;
        }

        /**
         * Method that changes the name of the OperationType.
         */
        public void ChangeOperationTypeName(OperationName name)
        {
            this.Name = name;
        }

        /**
         * Method that changes the required staff of the OperationType.
         */
        public void ChangeRequiredStaff(List<RequiredStaff> requiredStaff)
        {
            this.RequiredStaff = requiredStaff;
        }

        /**
         * Method that changes the estimated duration of the OperationType.
         */
        public void ChangeEstimatedDuration(List<EstimatedDuration> estimatedDuration)
        {
            this.EstimatedDuration = estimatedDuration;
        }

        /**
         * Method that activates the OperationType, marking it as active.
         */
        public void ActivateOperationType()
        {
            this.IsActive = true;
        }

        /**
         * Method that deactivates the OperationType, marking it as inactive.
         */
        public void DeactivateOperationType()
        {
            this.IsActive = false;
        }
    }
}
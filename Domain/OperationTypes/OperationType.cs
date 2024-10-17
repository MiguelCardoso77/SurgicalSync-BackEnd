using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.OperationTypes
{
    public class OperationType : Entity<OperationTypeId>, IAggregateRoot
    {
        public OperationTypeId Id { get; private set; }
        public OperationName Name { get; private set; }
        public List<RequiredStaff> RequiredStaff { get; private set; }
        public List<EstimatedDuration> EstimatedDuration { get; private set; }
        public bool IsActive { get; private set; }
        
        private OperationType()
        {
            this.Name = null;
            this.RequiredStaff = null;
            this.EstimatedDuration = null;
        }
        
        public OperationType(OperationTypeId id, OperationName name, List<RequiredStaff> requiredStaff, List<EstimatedDuration> estimatedDuration)
        {
            this.Id = id;
            this.Name = name;
            this.RequiredStaff = requiredStaff;
            this.EstimatedDuration = estimatedDuration;
            this.IsActive = true;
        }
        
        public void ChangeOperationTypeName(OperationName name)
        {
            this.Name = name;
        }
        
        public void ChangeRequiredStaff(List<RequiredStaff> requiredStaff)
        {
            this.RequiredStaff = requiredStaff;
        }
        
        public void ChangeEstimatedDuration(List<EstimatedDuration> estimatedDuration)
        {
            this.EstimatedDuration = estimatedDuration;
        }
        
        public void ActivateOperationType()
        {
            this.IsActive = true;
        }
        
        public void DeactivateOperationType()
        {
            this.IsActive = false;
        }
        
    }
}
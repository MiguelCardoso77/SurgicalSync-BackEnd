using System;
using System.Collections.Generic;
using DDDNetCore.Domain.OperationTypes;

namespace DDDNetCore.Application.DTO
{
    public class OperationTypeDto
    {
        public string Id { get; set; }
        public string OperationName { get; set; }
        public List<string> RequiredStaff { get; set; }
        public string EstimatedDuration { get; set; }
    }
}
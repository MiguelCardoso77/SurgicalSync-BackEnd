using System;

namespace DDDNetCore.Application.DTO
{
    public class OperationTypeDto
    {
        public String Id { get; set; }
        public String OperationName { get; set; }
        public String RequiredStaff { get; set; }
        public String EstimatedDuration { get; set; }
    }
}
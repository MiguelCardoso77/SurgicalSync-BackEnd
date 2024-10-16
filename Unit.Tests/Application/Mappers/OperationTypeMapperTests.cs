using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.Mappers
{
    [TestFixture]
    public class OperationTypeMapperTests
    {
        [Test]
        public void TestToDomain()
        {
            var requiredStaff = new List<string>() {"Nurse", "Doctor"};
            var estimatedDuration = new List<string>() {"30", "40"};
            var dto = new OperationTypeDto()
            {
                Id = "1", OperationName = "Test", RequiredStaff = requiredStaff, EstimatedDuration = estimatedDuration
            };
            
            var operationType = OperationTypeMapper.ToDomain(dto, new OperationTypeId(dto.Id), 
                dto.RequiredStaff.Select(rs => new RequiredStaff(rs)).ToList(), 
                dto.EstimatedDuration.Select(rs => new EstimatedDuration(rs)).ToList());
            
            Assert.AreEqual(operationType.Id.AsString(), dto.Id);
            Assert.AreEqual(operationType.Name.ToString(), dto.OperationName);
            Assert.AreEqual(operationType.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList(), requiredStaff);
            Assert.AreEqual(operationType.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList(), estimatedDuration);
        }
        
        [Test]
        public void TestToDto()
        {
            var requiredStaff = new List<RequiredStaff>() {new RequiredStaff("Nurse"), new RequiredStaff("Doctor")};
            var estimatedDuration = new List<EstimatedDuration>() {new EstimatedDuration("30"), new EstimatedDuration("40")};
            var operationType = new OperationType(new OperationTypeId("1"), new OperationName("Test"), requiredStaff, estimatedDuration);
            
            var dto = OperationTypeMapper.ToDto(operationType);
            
            Assert.AreEqual(dto.Id, operationType.Id.AsString());
            Assert.AreEqual(dto.OperationName, operationType.Name.ToString());
            Assert.AreEqual(dto.RequiredStaff, operationType.RequiredStaff.Select(rs => rs.RequiredStaffValue).ToList());
            Assert.AreEqual(dto.EstimatedDuration, operationType.EstimatedDuration.Select(rs => rs.EstimatedDurationValue).ToList());
        }
    }
}
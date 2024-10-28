using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.OperationType;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Mappers
{
    [TestFixture]
    public class OperationTypeMapperTests
    {
        private OperationTypeMapper _mapper;
        private Mock<OperationTypeId> _mockOperationTypeId;
        private Mock<OperationName> _mockOperationName;
        private Mock<RequiredStaff> _mockRequiredStaff;
        private Mock<EstimatedDuration> _mockDuration;
        
        [SetUp]
        public void Setup()
        {
            _mapper = new OperationTypeMapper();
            _mockOperationTypeId = new Mock<OperationTypeId>("1");
            _mockOperationName = new Mock<OperationName>("Test");
            _mockRequiredStaff = new Mock<RequiredStaff>("Nurse");
            _mockDuration = new Mock<EstimatedDuration>("30");
        }
        
        [Test]
        public void TestToDomain()
        {
            var dto = new OperationTypeDto()
            {
                Id = "1",
                OperationName = "Test",
                RequiredStaff = new List<string>() { "Nurse" },
                EstimatedDuration = new List<string>() { "30" }
            };
            
            var operationTypeId = new OperationTypeId(dto.Id);
            var requiredStaffList = dto.RequiredStaff.Select(rs => new RequiredStaff(rs)).ToList();
            var estimatedDurationList = dto.EstimatedDuration.Select(ed => new EstimatedDuration(ed)).ToList();

            var operationType = _mapper.ToDomain(dto, operationTypeId, requiredStaffList, estimatedDurationList);
            
            Assert.AreEqual(dto.Id, operationType.Id.AsString());
            Assert.AreEqual(dto.OperationName, operationType.Name.ToString());
            Assert.AreEqual(dto.RequiredStaff.First(), operationType.RequiredStaff.First().Value);
            Assert.AreEqual(dto.EstimatedDuration.First(), operationType.EstimatedDuration.First().Value.ToString());
        }

        [Test]
        public void TestToDto()
        {
            var operationType = new OperationType(
                _mockOperationTypeId.Object, 
                _mockOperationName.Object,
                new List<RequiredStaff>() { _mockRequiredStaff.Object }, 
                new List<EstimatedDuration>() { _mockDuration.Object }
            );
            
            var dto = _mapper.ToDto(operationType);
            
            Assert.AreEqual(_mockOperationTypeId.Object.AsString(), dto.Id);
            Assert.AreEqual(_mockOperationName.Object.ToString(), dto.OperationName);
            Assert.AreEqual(_mockRequiredStaff.Object.Value, dto.RequiredStaff.First());
            Assert.AreEqual(_mockDuration.Object.Value.ToString(), dto.EstimatedDuration.First());
        }
    }
}
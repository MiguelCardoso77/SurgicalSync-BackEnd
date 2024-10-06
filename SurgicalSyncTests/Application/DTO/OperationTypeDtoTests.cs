using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Application.DTO
{
    [TestFixture]
    public class OperationTypeDtoTests
    {
        [Test]
        public void TestCreateIncompleteOperationTypeDto()
        {
            var dto = new OperationTypeDto()
            {
                Id = "1",
                OperationName = "Test",
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.OperationName, "Test");
        }
        
        [Test]
        public void TestCreateCompleteOperationTypeDto()
        {
            var dto = new OperationTypeDto()
            {
                Id = "1",
                OperationName = "Test",
                RequiredStaff = new List<string>() {"Doctor", "Nurse"},
                EstimatedDuration = new List<string>()
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.OperationName, "Test");
            Assert.AreEqual(dto.RequiredStaff, new List<string>() {"Doctor", "Nurse"});
            Assert.AreEqual(dto.EstimatedDuration, new List<string>());
        }
        
    }
}
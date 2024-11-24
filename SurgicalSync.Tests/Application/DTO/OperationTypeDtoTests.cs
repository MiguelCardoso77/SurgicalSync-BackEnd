using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO
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
                RequiredStaff = "Doctor, Nurse",
                EstimatedDuration = "10, 20, 10"
            };

            Assert.AreEqual(dto.Id, "1");
            Assert.AreEqual(dto.OperationName, "Test");
            Assert.AreEqual(dto.RequiredStaff, "Doctor, Nurse");
            Assert.AreEqual(dto.EstimatedDuration, "10, 20, 10");
        }
        
    }
}
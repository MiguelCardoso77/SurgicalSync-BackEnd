using System;
using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO
{
    [TestFixture]
    public class OperationRequestDtoTests
    {
        [Test]
        public void TestCreateIncompleteOperationRequestDto()
        {
            var dto = new OperationRequestDto()
            {
                OperationRequestId = "1",
                DeadlineDate = "2025-01-07",
                Priority = "UrgentSurgery",
            };

            Assert.AreEqual(dto.OperationRequestId, "1");
            Assert.AreEqual(dto.DeadlineDate, "2025-01-07");
            Assert.AreEqual(dto.Priority, "UrgentSurgery");
            Assert.IsNull(dto.OperationTypeId);
            Assert.IsNull(dto.MedicalRecordNumber);
            Assert.IsNull(dto.StaffId);
        }
        
        [Test]
        public void TestCreateCompleteOperationRequestDto()
        {
            var dto = new OperationRequestDto()
            {
                OperationRequestId = "1",
                DeadlineDate = "2025-01-07",
                Priority = "UrgentSurgery",
                OperationTypeId = "2",
                MedicalRecordNumber = "202409000001",
                StaffId = "N202400001"
            };

            Assert.AreEqual(dto.OperationRequestId, "1");
            Assert.AreEqual(dto.DeadlineDate, "2025-01-07");
            Assert.AreEqual(dto.Priority, "UrgentSurgery");
            Assert.AreEqual(dto.OperationTypeId, "2");
            Assert.AreEqual(dto.MedicalRecordNumber, "202409000001");
            Assert.AreEqual(dto.StaffId, "N202400001");
        }
    }
}
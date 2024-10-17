using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Application.DTO
{
    [TestFixture]
    public class OperationRequestDtoTests
    {
        private OperationRequestDto _dto;

        [SetUp]
        public void SetUp()
        {
            _dto = new OperationRequestDto()
            {
                OperationRequestId = "1",
                DeadlineDate = "2025-10-01",
                Priority = "ElectiveSurgery",
                OperationTypeId = "2",
                MedicalRecordNumber = "123456",
                LicenseNumber = "D202400001"
            };
        }

        [Test]
        public void TestCreateValidOperationRequestDto()
        {
            Assert.AreEqual("1", _dto.OperationRequestId);
            Assert.AreEqual("2025-10-01", _dto.DeadlineDate);
            Assert.AreEqual("ElectiveSurgery", _dto.Priority);
            Assert.AreEqual("2", _dto.OperationTypeId);
            Assert.AreEqual("123456", _dto.MedicalRecordNumber);
            Assert.AreEqual("D202400001", _dto.LicenseNumber);
        }

        [Test]
        public void TestCreateIncompleteOperationRequestDto()
        {
            _dto.DeadlineDate = null;

            Assert.AreEqual("1", _dto.OperationRequestId);
            Assert.IsNull(_dto.DeadlineDate);
            Assert.AreEqual("ElectiveSurgery", _dto.Priority);
            Assert.AreEqual("2", _dto.OperationTypeId);
            Assert.AreEqual("123456", _dto.MedicalRecordNumber);
            Assert.AreEqual("D202400001", _dto.LicenseNumber);
        }

        [Test]
        public void TestCreateOperationRequestDtoWithNullValues()
        {
            _dto.OperationRequestId = null;
            _dto.DeadlineDate = null;
            _dto.Priority = null;
            _dto.OperationTypeId = null;
            _dto.MedicalRecordNumber = null;
            _dto.LicenseNumber = null;

            Assert.IsNull(_dto.OperationRequestId);
            Assert.IsNull(_dto.DeadlineDate);
            Assert.IsNull(_dto.Priority);
            Assert.IsNull(_dto.OperationTypeId);
            Assert.IsNull(_dto.MedicalRecordNumber);
            Assert.IsNull(_dto.LicenseNumber);
        }
    }
}
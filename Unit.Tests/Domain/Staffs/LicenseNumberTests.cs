using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class LicenseNumberTests
    {
        [Test]
        public void TestConstructor()
        {
            var licenseNumber = new LicenseNumber("N202400001");
            Assert.AreEqual("N202400001", licenseNumber.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var licenseNumber = new LicenseNumber("N202400001");
            Assert.AreEqual("N202400001", licenseNumber.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var licenseNumber1 = new LicenseNumber("N202400001");
            var licenseNumber2 = new LicenseNumber("N202400001");
            Assert.AreEqual(licenseNumber1, licenseNumber2);
        }
        
        
        [Test]
        public void TestDifferentHashCodes()
        {
            // Arrange
            var licenseNumber1 = new LicenseNumber("N202400001");
            var licenseNumber2 = new LicenseNumber("N202400002");

            // Act
            var hashCode1 = licenseNumber1.GetHashCode();
            var hashCode2 = licenseNumber2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
        }

    }
}
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
            var licenseNumber = new LicenseNumber("N20240001");
            Assert.AreEqual("N20240001", licenseNumber.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var licenseNumber = new LicenseNumber("N20240001");
            Assert.AreEqual("N20240001", licenseNumber.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var licenseNumber1 = new LicenseNumber("N20240001");
            var licenseNumber2 = new LicenseNumber("N20240001");
            Assert.AreEqual(licenseNumber1, licenseNumber2);
        }
    }
}
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class MedicalRecordNumberTests
    {
        [Test]
        public void TestConstructor()
        {
            var medicalRecordNumber = new MedicalRecordNumber("1000");
            Assert.AreEqual("1000", medicalRecordNumber.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var medicalRecordNumber = new MedicalRecordNumber("1000");
            Assert.AreEqual("1000", medicalRecordNumber.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var medicalRecordNumber1 = new MedicalRecordNumber("1000");
            var medicalRecordNumber2 = new MedicalRecordNumber("1000");
            Assert.AreEqual(medicalRecordNumber1, medicalRecordNumber2);
        }
    }
}
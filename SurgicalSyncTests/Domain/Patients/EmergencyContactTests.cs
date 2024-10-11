using DDDNetCore.Domain.OperationTypes;
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class EmergencyContactTests
    {
        [Test]
        public void TestConstructor()
        {
            var emergencyContact = new EmergencyContact("934118398");
            Assert.AreEqual("934118398", emergencyContact.EmergencyContactValue);
        }
        
        [Test]
        public void TestToString()
        {
            var emergencyContact = new EmergencyContact("934118398");
            Assert.AreEqual("934118398", emergencyContact.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var emergencyContact1 = new EmergencyContact("934118398");
            var emergencyContact2 = new EmergencyContact("934118398");
            Assert.AreEqual(emergencyContact1, emergencyContact2);
        }
    }
}
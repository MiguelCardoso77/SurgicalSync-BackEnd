using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class PatientNameTests
    {
        [Test]
        public void TestConstructor()
        {
            var patientName = new PatientName("Diana");
            Assert.AreEqual("Diana", patientName.PatientNameValue);
        }
        
        [Test]
        public void TestToString()
        {
            var patientName = new PatientName("Diana");
            Assert.AreEqual("Diana", patientName.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var patientName1 = new PatientName("Diana");
            var patientName2 = new PatientName("Diana");
            Assert.AreEqual(patientName1, patientName2);
        }

    }
}
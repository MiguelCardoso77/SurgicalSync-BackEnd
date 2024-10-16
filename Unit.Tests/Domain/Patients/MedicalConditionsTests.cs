using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class MedicalConditionsTests
    {
        [Test]
        public void TestConstructor()
        {
            var medicalConditions = new MedicalConditions("Asma");
            Assert.AreEqual("Asma", medicalConditions.MedicalConditionsValue);
        }
        
        [Test]
        public void TestToString()
        {
            var medicalConditions = new MedicalConditions("Asma");
            Assert.AreEqual("Asma", medicalConditions.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var medicalConditions1 = new MedicalConditions("Asma");
            var medicalConditions2 = new MedicalConditions("Asma");
            Assert.AreEqual(medicalConditions1, medicalConditions2);
        }
    }
}
using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class GenderTests
    {
        [Test]
        public void TestConstructor()
        {
            var gender = new Gender("masculino");
            Assert.AreEqual("masculino", gender.GenderValue);
        }
        
        [Test]
        public void TestToString()
        {
            var gender = new Gender("masculino");
            Assert.AreEqual("masculino", gender.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var gender1 = new Gender("masculino");
            var gender2 = new Gender("masculino");
            Assert.AreEqual(gender1, gender2);
        }
    }
}
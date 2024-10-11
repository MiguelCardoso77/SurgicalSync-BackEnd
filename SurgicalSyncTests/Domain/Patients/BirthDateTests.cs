using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class BirthDateTests
    {
        [Test]
        public void TestConstructor()
        {
            var birthDate = new BirthDate("30 de Junho de 2004");
            Assert.AreEqual("30 de Junho de 2004", birthDate.BirthDateValue);
        }
        
        [Test]
        public void TestToString()
        {
            var birthDate = new BirthDate("30 de Junho de 2004");
            Assert.AreEqual("30 de Junho de 2004", birthDate.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var birthDate1 = new BirthDate("30 de Junho de 2004");
            var birthDate2 = new BirthDate("30 de Junho de 2004");
            Assert.AreEqual(birthDate1, birthDate2);
        }
    }
}
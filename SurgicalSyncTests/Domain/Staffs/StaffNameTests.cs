using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffNameTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffName = new StaffName("Raquel Gonçalves");
            Assert.AreEqual("Raquel Gonçalves", staffName.StaffNameValue);
        }
        
        [Test]
        public void TestToString()
        {
            var staffName = new StaffName("Raquel Gonçalves");
            Assert.AreEqual("Raquel Gonçalves", staffName.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffName1 = new StaffName("Raquel Gonçalves");
            var staffName2 = new StaffName("Raquel Gonçalves");
            Assert.AreEqual(staffName1, staffName2);
        }
    }
}
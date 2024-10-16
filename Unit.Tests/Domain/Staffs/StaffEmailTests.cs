using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    public class StaffEmailTests
    {
        [Test]
        public void TestConstructor()
        {
            var staffEmail = new StaffEmail("raquelgoncalves@gmail.com");
            Assert.AreEqual("raquelgoncalves@gmail.com", staffEmail.StaffEmailValue);
        }
        
        [Test]
        public void TestToString()
        {
            var staffEmail = new StaffEmail("raquelgoncalves@gmail.com");
            Assert.AreEqual("raquelgoncalves@gmail.com", staffEmail.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var staffEmail1 = new StaffEmail("raquelgoncalves@gmail.com");
            var staffEmail2 = new StaffEmail("raquelgoncalves@gmail.com");
            Assert.AreEqual(staffEmail1, staffEmail2);
        }
    }
}
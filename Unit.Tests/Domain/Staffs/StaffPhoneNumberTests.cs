using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    [TestFixture]

    public class StaffPhoneNumberTests
    {
            [Test]
            public void TestConstructor()
            {
                var phoneNumber = new PhoneNumber("934568742");
                Assert.AreEqual("934568742", phoneNumber.PhoneNumberValue);
            }
        
            [Test]
            public void TestToString()
            {
                var phoneNumber = new PhoneNumber("934568742");
                Assert.AreEqual("934568742", phoneNumber.ToString());
            }
        
            [Test]
            public void TestEquals()
            {
                var phoneNumber1 = new PhoneNumber("934568742");
                var phoneNumber2 = new PhoneNumber("934568742");
                Assert.AreEqual(phoneNumber1, phoneNumber2);
            }
        
    }
}
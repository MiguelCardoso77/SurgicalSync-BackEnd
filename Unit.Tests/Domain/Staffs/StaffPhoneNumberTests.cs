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
        
            [Test]
            public void TestEqualHashCodes()
            {
                // Arrange
                var phoneNumber1 = new PhoneNumber("934568742");
                var phoneNumber2 = new PhoneNumber("934568742");
            
                // Act
                var hashCode1 = phoneNumber1.GetHashCode();
                var hashCode2 = phoneNumber2.GetHashCode();

                // Assert
                Assert.AreEqual(hashCode1, hashCode2, "Equal instances should have the same hash code");
            }
        
            [Test]
            public void TestDifferentHashCodes()
            {
                // Arrange
                var phoneNumber1 = new PhoneNumber("934568742");
                var phoneNumber2 = new PhoneNumber("931111111");
            
                // Act
                var hashCode1 = phoneNumber1.GetHashCode();
                var hashCode2 = phoneNumber2.GetHashCode();


                // Assert
                Assert.AreNotEqual(hashCode1, hashCode2, "Different instances should have different hash codes");
            }
    }
}
using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class RequiredStaffTests
    {
        [Test]
        public void TestConstructor()
        {
            var requiredStaff = new RequiredStaff("Surgeon");
            Assert.AreEqual("Surgeon", requiredStaff.RequiredStaffValue);
        }
        
        [Test]
        public void TestToString()
        {
            var requiredStaff = new RequiredStaff("Surgeon");
            Assert.AreEqual("Surgeon", requiredStaff.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var requiredStaff1 = new RequiredStaff("Surgeon");
            var requiredStaff2 = new RequiredStaff("Surgeon");
            Assert.AreEqual(requiredStaff1, requiredStaff2);
        }
        
    }
}
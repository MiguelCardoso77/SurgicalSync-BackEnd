using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class EstimatedDurationTests
    {
        [Test]
        public void TestConstructor()
        {
            var estimatedDuration = new EstimatedDuration("50");
            Assert.AreEqual("50", estimatedDuration.EstimatedDurationValue);
        }
        
        [Test]
        public void TestToString()
        {
            var estimatedDuration = new EstimatedDuration("1:30");
            Assert.AreEqual("1:30", estimatedDuration.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var estimatedDuration1 = new EstimatedDuration("1:30");
            var estimatedDuration2 = new EstimatedDuration("1:30");
            Assert.AreEqual(estimatedDuration1, estimatedDuration2);
        }
        
    }
}
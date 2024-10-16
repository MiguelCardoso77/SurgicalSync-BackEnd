using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class OperationNameTests
    {
        [Test]
        public void TestConstructor()
        {
            var operationName = new OperationName("Ankle Surgery");
            Assert.AreEqual("Ankle Surgery", operationName.OperationNameValue);
        }
        
        [Test]
        public void TestToString()
        {
            var operationName = new OperationName("Ankle Surgery");
            Assert.AreEqual("Ankle Surgery", operationName.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var operationName1 = new OperationName("Ankle Surgery");
            var operationName2 = new OperationName("Ankle Surgery");
            Assert.AreEqual(operationName1, operationName2);
        }
        
    }
}
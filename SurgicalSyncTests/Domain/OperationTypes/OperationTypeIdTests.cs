using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class OperationTypeIdTests
    {
        [Test]
        public void TestConstructor()
        {
            var operationTypeId = new OperationTypeId("1");
            Assert.AreEqual("1", operationTypeId.Value);
        }
        
        [Test]
        public void TestToString()
        {
            var operationTypeId = new OperationTypeId("1");
            Assert.AreEqual("1", operationTypeId.AsString());
        }
        
        [Test]
        public void TestEquals()
        {
            var operationTypeId1 = new OperationTypeId("1");
            var operationTypeId2 = new OperationTypeId("1");
            Assert.AreEqual(operationTypeId1, operationTypeId2);
        }
        
    }
}
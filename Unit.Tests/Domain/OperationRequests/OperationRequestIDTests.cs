using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class OperationRequestIdTests
    {
        [Test]
        public void Constructor_ValidString_ShouldCreateInstance()
        {
            var idValue = "12345";

            var operationRequestId = new OperationRequestId(idValue);

            Assert.NotNull(operationRequestId);
            Assert.AreEqual(idValue, operationRequestId.AsString());
        }

        [Test]
        public void AsString_ShouldReturnCorrectStringValue()
        {
            var idValue = "67890";
            var operationRequestId = new OperationRequestId(idValue);

            var result = operationRequestId.AsString();

            Assert.AreEqual(idValue, result);
        }

        [Test]
        public void Equals_SameId_ShouldReturnTrue()
        {
            var idValue = "11111";
            var operationRequestId1 = new OperationRequestId(idValue);
            var operationRequestId2 = new OperationRequestId(idValue);

            var result = operationRequestId1.Equals(operationRequestId2);

            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_DifferentId_ShouldReturnFalse()
        {
            var operationRequestId1 = new OperationRequestId("11111");
            var operationRequestId2 = new OperationRequestId("22222");

            var result = operationRequestId1.Equals(operationRequestId2);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetHashCode_SameId_ShouldReturnSameHashCode()
        {
            var idValue = "33333";
            var operationRequestId1 = new OperationRequestId(idValue);
            var operationRequestId2 = new OperationRequestId(idValue);

            var hash1 = operationRequestId1.GetHashCode();
            var hash2 = operationRequestId2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void GetHashCode_DifferentId_ShouldReturnDifferentHashCodes()
        {
            var operationRequestId1 = new OperationRequestId("44444");
            var operationRequestId2 = new OperationRequestId("55555");

            var hash1 = operationRequestId1.GetHashCode();
            var hash2 = operationRequestId2.GetHashCode();

            Assert.AreNotEqual(hash1, hash2);
        }
    }
}
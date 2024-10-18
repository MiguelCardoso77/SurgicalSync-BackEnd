using System;
using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class OperationRequestIdTests
    {
        [Test]
        [TestCase("12345")]
        [TestCase("67890")]
        [TestCase("ABCDE")]
        public void WhenInstantiatingWithValidString_ThenShouldCreateInstance(string idValue)
        {
            // act
            var operationRequestId = new OperationRequestId(idValue);

            // assert
            Assert.NotNull(operationRequestId);
            Assert.AreEqual(idValue, operationRequestId.AsString());
        }

        [Test]
        [TestCase("11111", "11111", true)]
        [TestCase("22222", "33333", false)]
        public void WhenComparingIds_ThenEqualsShouldReturnExpectedResult(string idValue1, string idValue2, bool expected)
        {
            // arrange
            var operationRequestId1 = new OperationRequestId(idValue1);
            var operationRequestId2 = new OperationRequestId(idValue2);

            // act
            var result = operationRequestId1.Equals(operationRequestId2);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("44444")]
        [TestCase("55555")]
        public void WhenGettingHashCodeForSameId_ThenShouldReturnSameHashCode(string idValue)
        {
            // arrange
            var operationRequestId1 = new OperationRequestId(idValue);
            var operationRequestId2 = new OperationRequestId(idValue);

            // act
            var hash1 = operationRequestId1.GetHashCode();
            var hash2 = operationRequestId2.GetHashCode();

            // assert
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        [TestCase("66666", "77777")]
        [TestCase("88888", "99999")]
        public void WhenGettingHashCodeForDifferentIds_ThenShouldReturnDifferentHashCodes(string idValue1, string idValue2)
        {
            // arrange
            var operationRequestId1 = new OperationRequestId(idValue1);
            var operationRequestId2 = new OperationRequestId(idValue2);

            // act
            var hash1 = operationRequestId1.GetHashCode();
            var hash2 = operationRequestId2.GetHashCode();

            // assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        [TestCase(null)]
        public void WhenInstantiatingWithInvalidString_ThenShouldThrowArgumentNullException(string idValue)
        {
            // assert
            var ex = Assert.Throws<NullReferenceException>(() => new OperationRequestId(idValue));
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public void WhenComparingWithNullObject_ThenEqualsShouldReturnFalse()
        {
            // arrange
            var operationRequestId = new OperationRequestId("11111");

            // act
            var result = operationRequestId.Equals(null);

            // assert
            Assert.False(result);
        }

        [Test]
        public void WhenComparingToObjectOfDifferentType_ThenEqualsShouldReturnFalse()
        {
            // arrange
            var operationRequestId = new OperationRequestId("11111");
            var anotherObject = new { Value = "11111" }; 

            // act
            var result = operationRequestId.Equals(anotherObject);

            // assert
            Assert.False(result);
        }
    }
}

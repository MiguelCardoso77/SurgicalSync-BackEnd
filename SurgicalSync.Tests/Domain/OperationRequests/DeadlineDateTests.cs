using System;
using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class DeadlineDateTests
    {
        [Test]
        [TestCase(5)]
        [TestCase(0)]
        public void WhenInstantiatingWithValidDates_ThenShouldCreateInstance(int daysToAdd)
        {
            // arrange
            var date = DateTime.Now.AddDays(daysToAdd);

            // act
            var deadline = new DeadlineDate(date);

            // assert
            Assert.NotNull(deadline);
            Assert.AreEqual(date.ToString("yyyy-MM-dd"), deadline.ToString());
        }

        [Test]
        [TestCase(-1)]
        public void WhenInstantiatingWithInvalidDates_ThenShouldThrowArgumentException(int daysToAdd)
        {
            // arrange
            var date = DateTime.Now.AddDays(daysToAdd);

            // act & assert
            var exception = Assert.Throws<ArgumentException>(() => new DeadlineDate(date));
            Assert.AreEqual("Deadline date cannot be in the past", exception.Message);
        }

        [Test]
        [TestCase(10, 10, true)]
        [TestCase(5, 10, false)]
        public void WhenComparingDates_ThenEqualsShouldReturnExpectedResult(int days1, int days2, bool expectedResult)
        {
            // arrange
            var deadline1 = new DeadlineDate(DateTime.Today.AddDays(days1));
            var deadline2 = new DeadlineDate(DateTime.Today.AddDays(days2));

            // act
            var result = deadline1.Equals(deadline2);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WhenComparingWithNullObject_ThenEqualsShouldReturnFalse()
        {
            // arrange
            var deadline = new DeadlineDate(DateTime.Now.AddDays(5));

            // act
            var result = deadline.Equals(null);

            // assert
            Assert.False(result);
        }

        [Test]
        [TestCase(10)]
        public void WhenGettingHashCodeForSameDates_ThenShouldReturnSameHashCode(int daysToAdd)
        {
            // arrange
            var date = DateTime.Now.AddDays(daysToAdd);
            var deadline1 = new DeadlineDate(date);
            var deadline2 = new DeadlineDate(date);

            // act
            var hash1 = deadline1.GetHashCode();
            var hash2 = deadline2.GetHashCode();

            // assert
            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        [TestCase(5, 10)]
        public void WhenGettingHashCodeForDifferentDates_ThenShouldReturnDifferentHashCodes(int days1, int days2)
        {
            // arrange
            var deadline1 = new DeadlineDate(DateTime.Now.AddDays(days1));
            var deadline2 = new DeadlineDate(DateTime.Now.AddDays(days2));

            // act
            var hash1 = deadline1.GetHashCode();
            var hash2 = deadline2.GetHashCode();

            // assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void WhenConvertingToString_ThenShouldReturnFormattedDate()
        {
            // arrange
            var date = DateTime.Now;
            var deadline = new DeadlineDate(date);

            // act
            var result = deadline.ToString();

            // assert
            var expectedDateString = date.ToString("yyyy-MM-dd");
            Assert.AreEqual(expectedDateString, result);
        }
    }
}

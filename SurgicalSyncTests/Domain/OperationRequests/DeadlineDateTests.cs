using System;
using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationRequests
{
    [TestFixture]
    public class DeadlineDateTests
    {
        [Test]
        public void Constructor_ValidFutureDate_ShouldCreateInstance()
        {
            var futureDate = DateTime.Now.AddDays(5);

            var deadline = new DeadlineDate(futureDate);

            Assert.NotNull(deadline);
            Assert.AreEqual(futureDate.ToString("yyyy-MM-dd"), deadline.ToString());
        }
        [Test]
        public void Constructor_PastDate_ShouldThrowArgumentException()
        {
            var pastDate = DateTime.Now.AddDays(-1);

            var exception = Assert.Throws<ArgumentException>(() => new DeadlineDate(pastDate));
            Assert.AreEqual("Deadline date cannot be in the past", exception.Message);
        }

        [Test]
        public void Constructor_CurrentDate_ShouldCreateInstance()
        {
            var currentDate = DateTime.Now;

            var deadline = new DeadlineDate(currentDate);

            Assert.NotNull(deadline);
            Assert.AreEqual(currentDate.ToString("yyyy-MM-dd"), deadline.ToString());
        }

        [Test]
        public void Equals_SameDate_ShouldReturnTrue()
        {
            var date = DateTime.Now.AddDays(10);
            var deadline1 = new DeadlineDate(date);
            var deadline2 = new DeadlineDate(date);

            var result = deadline1.Equals(deadline2);

            Assert.True(result);
        }

        [Test]
        public void Equals_DifferentDate_ShouldReturnFalse()
        {
            var deadline1 = new DeadlineDate(DateTime.Now.AddDays(5));
            var deadline2 = new DeadlineDate(DateTime.Now.AddDays(10));

            var result = deadline1.Equals(deadline2);

            Assert.False(result);
        }

        [Test]
        public void Equals_NullObject_ShouldReturnFalse()
        {
            var deadline = new DeadlineDate(DateTime.Now.AddDays(5));

            var result = deadline.Equals(null);
            
            Assert.False(result);
        }

        [Test]
        public void GetHashCode_SameDate_ShouldReturnSameHashCode()
        {
            var date = DateTime.Now.AddDays(10);
            var deadline1 = new DeadlineDate(date);
            var deadline2 = new DeadlineDate(date);

            var hash1 = deadline1.GetHashCode();
            var hash2 = deadline2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void GetHashCode_DifferentDates_ShouldReturnDifferentHashCodes()
        {
            var deadline1 = new DeadlineDate(DateTime.Now.AddDays(5));
            var deadline2 = new DeadlineDate(DateTime.Now.AddDays(10));

            var hash1 = deadline1.GetHashCode();
            var hash2 = deadline2.GetHashCode();

            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void ToString_ShouldReturnFormattedDate()
        {
            var date = DateTime.Now;
            var deadline = new DeadlineDate(date);

            var result = deadline.ToString();

            var expectedDateString = date.ToString("yyyy-MM-dd");
            Assert.AreEqual(expectedDateString, result);
        }
    }
}

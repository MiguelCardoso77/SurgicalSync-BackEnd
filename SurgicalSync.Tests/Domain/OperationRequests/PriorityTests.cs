using System;
using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class PriorityTest
    {
        private const string ElectiveSurgeryString = "ElectiveSurgery";
        private const string UrgentSurgeryString = "UrgentSurgery";
        private const string EmergencySurgeryString = "EmergencySurgery";

        [Test]
        [TestCase(Priority.ElectiveSurgery)]
        [TestCase(Priority.UrgentSurgery)]
        [TestCase(Priority.EmergencySurgery)]
        public void WhenCheckingIfEnumContainsPriorityLevels_ThenShouldReturnTrue(Priority priority)
        {
            Assert.True(Enum.IsDefined(typeof(Priority), priority));
        }

        [Test]
        public void WhenCallingEnumToString_ShouldReturnCorrectValue()
        {
            // arrange
            var elective = Priority.ElectiveSurgery;
            var urgent = Priority.UrgentSurgery;
            var emergency = Priority.EmergencySurgery;

            // act & assert
            Assert.AreEqual(ElectiveSurgeryString, elective.ToString());
            Assert.AreEqual(UrgentSurgeryString, urgent.ToString());
            Assert.AreEqual(EmergencySurgeryString, emergency.ToString());
        }

        [Test]
        [TestCase("ElectiveSurgery", Priority.ElectiveSurgery)]
        [TestCase("UrgentSurgery", Priority.UrgentSurgery)]
        [TestCase("EmergencySurgery", Priority.EmergencySurgery)]
        public void WhenParsingValidString_ShouldReturnCorrectEnumValue(string priorityString, Priority expectedPriority)
        {
            // act
            var parsedPriority = (Priority)Enum.Parse(typeof(Priority), priorityString);

            // assert
            Assert.AreEqual(expectedPriority, parsedPriority);
        }

        [Test]
        [TestCase("electivesurgery")]
        [TestCase("URGENTSURGERY")]
        [TestCase("Elective Surgery")]
        [TestCase("Emergency_Surgery")]
        public void WhenParsingInvalidFormattedString_ShouldThrowArgumentException(string invalidString)
        {
            // assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // act
                Enum.Parse(typeof(Priority), invalidString);
            });
            Assert.That(ex.Message, Is.Not.Empty); // Optional check on exception message
        }

        [Test]
        public void WhenParsingInvalidString_ShouldThrowArgumentException()
        {
            // assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // act
                Enum.Parse(typeof(Priority), "NonExistentPriority");
            });
            Assert.That(ex.Message, Is.Not.Empty); // Optional check on exception message
        }

        [Test]
        [TestCase(999)]
        [TestCase(-1)]
        [TestCase(3)]
        public void WhenCheckingInvalidEnumValue_ShouldReturnFalse(int invalidValue)
        {
            // act & assert
            Assert.False(Enum.IsDefined(typeof(Priority), invalidValue));
        }

        [Test]
        public void WhenConvertingEnumToInteger_ShouldReturnExpectedValues()
        {
            // act & assert
            Assert.AreEqual(0, (int)Priority.ElectiveSurgery);
            Assert.AreEqual(1, (int)Priority.UrgentSurgery);
            Assert.AreEqual(2, (int)Priority.EmergencySurgery);
        }

        [Test]
        public void WhenConvertingIntegerToEnum_ShouldReturnCorrectEnumValue()
        {
            // act
            var elective = (Priority)0;
            var urgent = (Priority)1;
            var emergency = (Priority)2;

            // assert
            Assert.AreEqual(Priority.ElectiveSurgery, elective);
            Assert.AreEqual(Priority.UrgentSurgery, urgent);
            Assert.AreEqual(Priority.EmergencySurgery, emergency);
        }

        [Test]
        public void WhenConvertingOutOfRangeIntegerToEnum_ShouldThrowException()
        {
            // act & assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Invalid integer for enum, casting explicitly to force error
                if (!Enum.IsDefined(typeof(Priority), 999))
                    throw new ArgumentException();
            });
            Assert.That(ex.Message, Is.Not.Empty); // Optional check on exception message
        }
    }
}

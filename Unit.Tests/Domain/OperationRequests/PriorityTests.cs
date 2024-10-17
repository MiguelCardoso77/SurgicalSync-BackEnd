using System;
using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.OperationRequests
{
    [TestFixture]
    public class PriorityTest
    {
        
        private const string ElectiveSurgeryString = "ElectiveSurgery";
        private const string UrgentSurgeryString = "UrgentSurgery";
        private const string EmergencySurgeryString = "EmergencySurgery";
        
        [Test]
        public void Enum_Contains_AllPriorityLevels()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(Priority), Priority.ElectiveSurgery));
            Assert.IsTrue(Enum.IsDefined(typeof(Priority), Priority.UrgentSurgery));
            Assert.IsTrue(Enum.IsDefined(typeof(Priority), Priority.EmergencySurgery));
        }
        
        [Test]
        public void Enum_ToString_ShouldReturnCorrectValue()
        {
            var elective = Priority.ElectiveSurgery;
            var urgent = Priority.UrgentSurgery;
            var emergency = Priority.EmergencySurgery;

            Assert.AreEqual(ElectiveSurgeryString, elective.ToString());
            Assert.AreEqual(UrgentSurgeryString, urgent.ToString());
            Assert.AreEqual(EmergencySurgeryString, emergency.ToString());
        }
        
        [Test]
        public void Parse_ValidString_ShouldReturnCorrectEnumValue()
        {
            var elective = (Priority)Enum.Parse(typeof(Priority), ElectiveSurgeryString);
            var urgent = (Priority)Enum.Parse(typeof(Priority), UrgentSurgeryString);
            var emergency = (Priority)Enum.Parse(typeof(Priority), EmergencySurgeryString);

            Assert.AreEqual(Priority.ElectiveSurgery, elective);
            Assert.AreEqual(Priority.UrgentSurgery, urgent);
            Assert.AreEqual(Priority.EmergencySurgery, emergency);
        }
        [Test]
        public void Parse_InvalidString_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => Enum.Parse(typeof(Priority), "NonExistentPriority"));        
        }
    }
}
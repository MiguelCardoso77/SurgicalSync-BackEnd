using System;
using DDDNetCore.Domain.OperationRequests;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationRequests
{
    [TestFixture]
    public class PriorityTest
    {
        [Test]
        public void Enum_Contains_AllPriorityLevels()
        {
            Assert.AreEqual(3, Enum.GetValues(typeof(Priority)).Length);
            Assert.IsTrue(Enum.IsDefined(typeof(Priority), "ElectiveSurgery"));
            Assert.IsTrue(Enum.IsDefined(typeof(Priority), "UrgentSurgery"));
            Assert.IsTrue(Enum.IsDefined(typeof(Priority), "EmergencySurgery"));
        }
        
        [Test]
        public void Enum_ToString_ShouldReturnCorrectValue()
        {
            var elective = Priority.ElectiveSurgery;
            var urgent = Priority.UrgentSurgery;
            var emergency = Priority.EmergencySurgery;

            Assert.AreEqual("ElectiveSurgery", elective.ToString());
            Assert.AreEqual("UrgentSurgery", urgent.ToString());
            Assert.AreEqual("EmergencySurgery", emergency.ToString());
        }
        
        [Test]
        public void Parse_ValidString_ShouldReturnCorrectEnumValue()
        {
            var elective = (Priority)Enum.Parse(typeof(Priority), "ElectiveSurgery");
            var urgent = (Priority)Enum.Parse(typeof(Priority), "UrgentSurgery");
            var emergency = (Priority)Enum.Parse(typeof(Priority), "EmergencySurgery");

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
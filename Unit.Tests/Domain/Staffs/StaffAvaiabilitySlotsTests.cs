using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Staffs
{
    [TestFixture]

    public class StaffAvaiabilitySlotsTests
    {
            [Test]
            public void TestConstructor()
            {
                var avaiabilitySlots = new StaffAvaiabilitySlots("2024-09-25:14h00-18h00");
                Assert.AreEqual("2024-09-25:14h00-18h00", avaiabilitySlots.StaffAvaiabilitySlotsValue);
            }
        
            [Test]
            public void TestToString()
            {
                var avaiabilitySlots = new StaffAvaiabilitySlots("2024-09-25:14h00-18h00");
                Assert.AreEqual("2024-09-25:14h00-18h00", avaiabilitySlots.ToString());
            }
        
            [Test]
            public void TestEquals()
            {
                var avaiabilitySlots1 = new AppointmentHistory("2024-09-25:14h00-18h00");
                var avaiabilitySlots2 = new AppointmentHistory("2024-09-25:14h00-18h00");
                Assert.AreEqual(avaiabilitySlots1, avaiabilitySlots2);
            }

    }
}
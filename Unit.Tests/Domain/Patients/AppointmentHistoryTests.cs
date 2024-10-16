using DDDNetCore.Domain.Patients;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.Patients
{
    [TestFixture]
    public class AppointmentHistoryTests
    {
        [Test]
        public void TestConstructor()
        {
            var appointmentHistory = new AppointmentHistory("Surgeon");
            Assert.AreEqual("Surgeon", appointmentHistory.AppointmentHistoryValue);
        }
        
        [Test]
        public void TestToString()
        {
            var appointmentHistory = new AppointmentHistory("Surgeon");
            Assert.AreEqual("Surgeon", appointmentHistory.ToString());
        }
        
        [Test]
        public void TestEquals()
        {
            var appointmentHistory1 = new AppointmentHistory("Surgeon");
            var appointmentHistory2 = new AppointmentHistory("Surgeon");
            Assert.AreEqual(appointmentHistory1, appointmentHistory2);
        }

    }
}
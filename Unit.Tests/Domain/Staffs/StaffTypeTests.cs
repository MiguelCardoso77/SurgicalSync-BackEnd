using DDDNetCore.Domain.Staffs;
using NUnit.Framework;

namespace DDDNetCore.Unit.Tests.Domain.Staffs
{
    public class StaffTypeTests
    {
        [Test]
        public void TestPrivateConstructor()
        {
            var staffType = StaffType.Other;

            Assert.NotNull(staffType);
        }
    }
}
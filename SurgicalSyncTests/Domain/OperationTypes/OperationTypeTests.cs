using System.Collections.Generic;
using DDDNetCore.Domain.OperationTypes;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Domain.OperationTypes
{
    [TestFixture]
    public class OperationTypeTests
    {
        [Test]
        public void TestConstructor()
        {
            var operationType = new OperationType(
                new OperationTypeId("1"),
                new OperationName("Surgery"),
                new List<RequiredStaff>(),
                new List<EstimatedDuration>()
            );
            
            Assert.AreEqual("1", operationType.Id.AsString());
            Assert.AreEqual("Surgery", operationType.Name.OperationNameValue);
            Assert.AreEqual(0, operationType.RequiredStaff.Count);
            Assert.AreEqual(0, operationType.EstimatedDuration.Count);
        }
        
    }
}
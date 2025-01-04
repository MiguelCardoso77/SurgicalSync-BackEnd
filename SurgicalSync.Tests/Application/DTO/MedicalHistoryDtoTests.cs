using DDDNetCore.Application.DTO;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.DTO;

[TestFixture]
public class MedicalHistoryDtoTests
{
    [Test]
    public void TestCreateIncompleteMedicalHistoryDto()
    {
        // Arrange
        var dto = new MedicalHistoryDto()
        {
            PatientName = "John Doe",
            PhoneNumber = "123-456-7890"
        };
        
        // Assert
        Assert.AreEqual(dto.PatientName, "John Doe");
        Assert.AreEqual(dto.PhoneNumber, "123-456-7890");
    }

    [Test]
    public void TestCreateCompleteMedicalHistoryDto()
    {
        // Arrange
        var dto = new MedicalHistoryDto()
        {
            PatientName = "John Doe",
            BirthDate = "01/01/2000",
            Gender = "Male",
            EmergencyContact = "Jane Doe",
            PhoneNumber = "123-456-7890"
        };

        // Assert
        Assert.AreEqual(dto.PatientName, "John Doe");
        Assert.AreEqual(dto.BirthDate, "01/01/2000");
        Assert.AreEqual(dto.Gender, "Male");
        Assert.AreEqual(dto.EmergencyContact, "Jane Doe");
        Assert.AreEqual(dto.PhoneNumber, "123-456-7890");
    }
}
namespace DDDNetCore.Application.DTO;

/**
 * This class is a Data Transfer Object (DTO) that represents the MedicalHistory entity.
 * It is used to transfer data between the Application and the Infrastructure layers.
 */
public class MedicalHistoryDto
{
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string EmergencyContact { get; set; }
}
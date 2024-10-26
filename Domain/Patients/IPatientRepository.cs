using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    /**
     * Represents a repository interface for managing patient entities.
     * This interface extends the IRepository interface with the specific
     * types for Patient and MedicalRecordNumber.
     */
    public interface IPatientRepository : IRepository<Patient, MedicalRecordNumber>
    {
    }
}
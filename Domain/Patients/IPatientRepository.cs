using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public interface IPatientRepository : IRepository<Patient, PatientId>
    {
        
    }
}
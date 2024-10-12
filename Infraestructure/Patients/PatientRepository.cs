using DDDNetCore.Domain.Patients;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace DDDNetCore.Infraestructure.Patients
{
    public class PatientRepository : BaseRepository<Patient, MedicalRecordNumber>, IPatientRepository
    {
        public PatientRepository(DDDSample1DbContext context) : base(context.Patients)
        {
            
        }
        
    }
}
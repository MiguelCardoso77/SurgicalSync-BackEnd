using DDDNetCore.Domain.Patients;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.Patients
{
    /**
     * Represents a repository for managing Patient entities in the data store.
     *
     * This class extends the BaseRepository to provide CRUD operations
     * specifically for the Patient entity, identified by the MedicalRecordNumber.
     * It encapsulates the data access logic required to interact with the
     * underlying data context.
     */
    public class PatientRepository : BaseRepository<Patient, MedicalRecordNumber>, IPatientRepository
    {
        /**
         * Initializes a new instance of the PatientRepository class.
         *
         * @param context The database context to be used for accessing
         *                the Patients DbSet. This context is provided
         *                by the Entity Framework Core and is used to
         *                perform CRUD operations on the Patient entities.
         */
        public PatientRepository(DDDSample1DbContext context) : base(context.Patients)
        {
        }
    }
}
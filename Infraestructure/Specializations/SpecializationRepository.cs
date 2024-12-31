using DDDNetCore.Domain.Specializations;
using DDDNetCore.Infraestructure.Shared;

namespace DDDNetCore.Infraestructure.Specializations;

public class SpecializationRepository: BaseRepository<Specialization, SpecializationCode>, ISpecializationRepository
{
    public SpecializationRepository(SurgicalSyncContext context):base(context.Specializations)
    {
            
    }
}
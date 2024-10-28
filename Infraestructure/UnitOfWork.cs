using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Infraestructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SurgicalSyncContext _context;

        public UnitOfWork(SurgicalSyncContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
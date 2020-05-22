using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IDeparmentTypeRepository : IRepository<DeparmentType>
    {
    }

    public class DeparmentTypeRepository : RepositoryBase<DeparmentType>, IDeparmentTypeRepository
    {
        public DeparmentTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

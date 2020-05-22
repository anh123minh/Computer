using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IProducerTypeRepository : IRepository<ProducerType>
    {
    }

    public class ProducerTypeRepository : RepositoryBase<ProducerType>, IProducerTypeRepository
    {
        public ProducerTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IComputerTypeRepository : IRepository<ComputerType>
    {
    }

    public class ComputerTypeRepository : RepositoryBase<ComputerType>, IComputerTypeRepository
    {
        public ComputerTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

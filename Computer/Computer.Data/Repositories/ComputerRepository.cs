using Computer.Data.Infrastructure;

namespace Computer.Data.Repositories
{
    public interface IComputerRepository : IRepository<Model.Models.Computer>
    {
    }

    public class ComputerRepository : RepositoryBase<Model.Models.Computer>, IComputerRepository
    {
        public ComputerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

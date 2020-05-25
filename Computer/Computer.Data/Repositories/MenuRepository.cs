using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IMenuRepository : IRepository<Function>
    {
    }

    public class MenuRepository : RepositoryBase<Function>, IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IColorRepository : IRepository<Color>
    {
    }

    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
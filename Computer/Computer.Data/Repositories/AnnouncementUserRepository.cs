using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IAnnouncementUserRepository : IRepository<AnnouncementUser>
    {
    }

    public class AnnouncementUserRepository : RepositoryBase<AnnouncementUser>, IAnnouncementUserRepository
    {
        public AnnouncementUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
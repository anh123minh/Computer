using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
    }

    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
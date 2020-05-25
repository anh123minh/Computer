using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{

    public interface IProductQuantityRepository : IRepository<ProductQuantity>
    {
    }

    public class ProductQuantityRepository : RepositoryBase<ProductQuantity>, IProductQuantityRepository
    {
        public ProductQuantityRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

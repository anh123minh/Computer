using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
    }

    public class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

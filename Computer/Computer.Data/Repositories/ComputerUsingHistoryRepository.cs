using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Computer.Common.Extentions;
using Computer.Data.Infrastructure;
using Computer.Model.Models;

namespace Computer.Data.Repositories
{
    public interface IComputerUsingHistoryRepository : IRepository<ComputerUsingHistory>
    {
        IEnumerable<ComputerUsingHistory> GetAllPagingWithFilterDeparmentTypeId(int pageIndex, int pageSize, out int totalRow, int? deparmentTypeId, string filter ="");
    }

    public class ComputerUsingHistoryRepository : RepositoryBase<ComputerUsingHistory>, IComputerUsingHistoryRepository
    {
        public ComputerUsingHistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ComputerUsingHistory> GetAllPagingWithFilterDeparmentTypeId(int pageIndex, int pageSize, out int totalRow, int? deparmentTypeId, string filter = "")
        {
            Expression<Func<ComputerUsingHistory, bool>> exp = x => true;
            if (!string.IsNullOrEmpty(filter))
            {
                exp = exp.AndWith(x => x.Computer.ComputerCode.Contains(filter) || x.Computer.ComputerName.Contains(filter));                
            }

            if (deparmentTypeId.HasValue)
            {
                exp = exp.AndWith(x => x.Computer.DeparmentTypeId == deparmentTypeId);
            }

            var query = DbContext.ComputerUsingHistories.Where(exp);

            totalRow = query.Count();

            query = query.OrderByDescending(x => x.UpdatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query.Include(x => x.Computer).Include(x => x.Computer.DeparmentType);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Computer.Common.Extentions;
using Computer.Data.Infrastructure;
using Computer.Data.Repositories;

namespace Computer.Data.Repositories
{
    public interface IComputerRepository : IRepository<Model.Models.Computer>
    {
        IEnumerable<ComputerStatisticByComputerType> GetComputerStatisticByComputerType();

        IEnumerable<ComputerStatisticByProducerType> GetComputerStatisticByProducerType();

        IEnumerable<ComputerStatisticByUsingUnit> GetComputerStatisticByUsingUnit();

        IEnumerable<Model.Models.Computer> GetAllPagingWithMultiFilters(int pageIndex, int pageSize, out int totalRow, int? computerTypeId, int? deparmanetTypeId, int? producerTypeId, string filter = "");
    }
}

public class ComputerRepository : RepositoryBase<Computer.Model.Models.Computer>, IComputerRepository
{
    public ComputerRepository(IDbFactory dbFactory) : base(dbFactory)
    {
    }

    public IEnumerable<ComputerStatisticByComputerType> GetComputerStatisticByComputerType()
    {
        var totalComputer = DbContext.Computers.Count();

        var query = DbContext.Computers.GroupBy(x => x.ComputerType.ComputerTypeName).Select(x => new ComputerStatisticByComputerType
        {
            CompterTypeName = x.Key,
            //Percent = x.Count()
            Percent = Math.Round((double)x.Count() / totalComputer * 100, 3) 
        });

        return query.OrderBy(x => x.CompterTypeName).ToList();
    }

    public IEnumerable<ComputerStatisticByProducerType> GetComputerStatisticByProducerType()
    {
        var totalComputer = DbContext.Computers.Count();

        var query = DbContext.Computers.GroupBy(x => x.ProducerType.ProducerTypeName).Select(x => new ComputerStatisticByProducerType
        {
            ProducerTypeName = x.Key,
            //Percent = x.Count()
            Percent = Math.Round((double)x.Count() / totalComputer * 100, 3) 
        });

        return query.OrderBy(x => x.ProducerTypeName).ToList();
    }

    public IEnumerable<ComputerStatisticByUsingUnit> GetComputerStatisticByUsingUnit()
    {
        var totalHour = DbContext.ComputerUsingHistories.Sum(x => DbFunctions.DiffHours(x.StartTime, x.EndTime)).Value;
        var query = DbContext.ComputerUsingHistories.GroupBy(x => x.Computer.ComputerName).Select(x => new ComputerStatisticByUsingUnit
        {
            ComputerName = x.Key,
            //UsingUnit = x.Sum(y => DbFunctions.DiffHours(y.StartTime, y.EndTime).Value)
            Percent = Math.Round((double)(x.Sum(y => DbFunctions.DiffHours(y.StartTime, y.EndTime).Value)) / totalHour * 100, 3) 
        });

        return query.OrderBy(x => x.ComputerName).ToList();
    }

    public IEnumerable<Computer.Model.Models.Computer> GetAllPagingWithMultiFilters(int pageIndex, int pageSize, out int totalRow,
        int? computerTypeId, int? deparmanetTypeId, int? producerTypeId, string filter = "")
    {
        //todo: check Id == 0
        Expression<Func<Computer.Model.Models.Computer, bool>> exp = x => true;
        
        if (!string.IsNullOrEmpty(filter))
        {
            exp = exp.AndWith(x => x.ComputerCode.Contains(filter) || x.ComputerName.Contains(filter));
        }

        if (computerTypeId.HasValue)
        {
            exp = exp.AndWith(x => x.ComputerTypeId == computerTypeId.Value);
        }

        if (deparmanetTypeId.HasValue)
        {
            exp = exp.AndWith(x => x.DeparmentTypeId == deparmanetTypeId.Value);
        }

        if (producerTypeId.HasValue)
        {
            exp = exp.AndWith(x => x.ProducerTypeId == producerTypeId.Value);
        }

        var query = DbContext.Computers.Where(exp);

        totalRow = query.Count();

        query = query.OrderByDescending(x => x.UpdatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return query.Include(x => x.ComputerType).Include(x => x.DeparmentType).Include(x => x.ProducerType);
    }
}

public class ComputerStatisticByComputerType
{
    public string CompterTypeName { get; set; }

    public double Percent { get; set; }
}

public class ComputerStatisticByProducerType
{
    public string ProducerTypeName { get; set; }

    public double Percent { get; set; }
}

public class ComputerStatisticByUsingUnit
{
    public string ComputerName { get; set; }

    public double Percent { get; set; }
}

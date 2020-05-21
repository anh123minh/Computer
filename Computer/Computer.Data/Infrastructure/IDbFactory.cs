using System;

namespace Computer.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ComputerDbContext Init();
    }
}
namespace Computer.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ComputerDbContext dbContext;

        public ComputerDbContext Init()
        {
            return dbContext ?? (dbContext = new ComputerDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
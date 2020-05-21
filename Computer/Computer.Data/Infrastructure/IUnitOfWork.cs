namespace Computer.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
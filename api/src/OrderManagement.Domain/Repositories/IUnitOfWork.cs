namespace OrderManagement.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}

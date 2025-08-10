using OrderManagement.Domain.Repositories;

namespace OrderManagement.Infra.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _dbContext;
        public UnitOfWork(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

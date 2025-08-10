using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;

namespace OrderManagement.Infra.DataAccess.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementDbContext _dbContext;
        public OrderRepository(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> Add(Order order)
        {
            var entry = await _dbContext.Orders.AddAsync(order);

            return entry.Entity;
        }

        public void Delete(Order order)
        {
            _dbContext.Orders.Remove(order);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetById(Guid id, bool withAsNoTracking = true)
        {
            if (withAsNoTracking) 
            {
                return await _dbContext.Orders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
            }

            return await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Order order)
        {
            _dbContext.Orders.Update(order);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infra.DataAccess
{
    public class OrderManagementDbContext : DbContext
    {
        public OrderManagementDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}

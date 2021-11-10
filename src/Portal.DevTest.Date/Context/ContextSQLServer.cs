using Microsoft.EntityFrameworkCore;
using Portal.DevTest.Date.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.DevTest.Date.Context
{
    public class ContextSQLServer : DbContext
    {
        public ContextSQLServer(DbContextOptions options) : base(options) 
        {
            this.Database.EnsureCreated();
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrdersItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextSQLServer).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}

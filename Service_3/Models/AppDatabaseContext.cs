using Microsoft.EntityFrameworkCore;

namespace Service_1.Models
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext() { }

        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options) { }
        
        public DbSet<OutBox> OutBoxes { get; set; }

        public DbSet<OrdersCar> OrdersCars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Web3Sem_Service_1;User Id=ilya;Password=123");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
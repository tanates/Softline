using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SoftlineTest.Models.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<ModelTasks> Tasks { get; set; }
        public DbSet<ModelStatus> Statuses { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelStatus>().HasData(
                new ModelStatus { ID = 1, Name = "Создана" },
                new ModelStatus { ID = 2, Name = "В работе" },
                new ModelStatus { ID = 3, Name = "Завершена" }
            );
        }
    }
}

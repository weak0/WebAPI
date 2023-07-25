using Microsoft.EntityFrameworkCore;

namespace WebAPI.Entities
{
    public class RestaurantDbContext: DbContext
    {
        private string _connectionString = "Server=MACIEK\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=true;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Addres> Adresses { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .HasMaxLength(25);

 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}

using Microsoft.EntityFrameworkCore;

namespace AssetTracker
{
    internal class MyDbContext : DbContext
    {
        string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=efdb1;Integrated Security=True;";
        
        public DbSet<Assets> Assets { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Assets>().HasData(
                new Assets { Id = 1, Asset_type = "Computer" },
                new Assets { Id = 2, Asset_type = "Phone" }
                );
        }

    }
}

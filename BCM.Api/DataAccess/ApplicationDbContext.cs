using BCM.Api.DataAccess.Configurations;
using BCM.Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCM.Api.DataAccess;

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
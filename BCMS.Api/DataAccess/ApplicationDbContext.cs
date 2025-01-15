using BCMS.Api.DataAccess.Configurations;
using BCMS.Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCMS.Api.DataAccess;

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
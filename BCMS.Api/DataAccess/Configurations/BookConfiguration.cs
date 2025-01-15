using BCMS.Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BCMS.Api.DataAccess.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        //? Indexes, etc?
        ///? COLLATE NOCASE for search
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(b => b.Title).IsRequired().HasMaxLength(255);
        builder.Property(b => b.Author).IsRequired().HasMaxLength(255);
        builder.Property(b => b.Genre).IsRequired().HasMaxLength(50);
    }
}
using EFExceptionSchema.Entities;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFExceptionSchema;
public class TestDbContext : DbContext
{
    public DbSet<Entities.Inventory.Category> InventoryCategories => Set<Entities.Inventory.Category>();
    public DbSet<Entities.Incidents.Category> IncidentCategories => Set<Entities.Incidents.Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // Use database 'EFCoreExceptions' in a local SQL Server installation.
        optionsBuilder.UseSqlServer("Database=EFCoreExceptions;Server=.;Integrated Security=true;TrustServerCertificate=true");
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table [Inventory].[Category] and [Incidents].[Category] have both a index names [IX_Category_Name]. This is allowed
        // in SQL Server because indexes are scoped to their associated tables.
        // Both tables are in different schemas.

        modelBuilder.Entity<Entities.Inventory.Category>(x =>
        {
            x.ToTable("Category", "Inventory");
            x.HasIndex(x => x.Name).IsUnique();
            x.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Entities.Incidents.Category>(x =>
        {
            x.ToTable("Category", "Incidents");
            x.HasIndex(x => x.Name).IsUnique();
            x.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });
    }
}

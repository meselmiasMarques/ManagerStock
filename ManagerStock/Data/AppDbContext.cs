using ManagerStock.Data.Mappings;
using ManagerStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerStock.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } 
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

         modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}
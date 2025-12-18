using ManagerStock.Data.Mappings;
using ManagerStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerStock.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Products> Products { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());
    }
}
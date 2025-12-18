using ManagerStock.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagerStock.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();
        
        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(50)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)");
        
        builder.Property(p => p.Description)
            .HasMaxLength(100)
            .HasColumnName("Description")
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Price)
            .IsRequired(true)
            .HasColumnName("Price")
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.StockAmout)
            .IsRequired(true)
            .HasColumnName("StockAmount")
            .HasColumnType("INTEGER")
            .HasDefaultValue(1);
        
        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");
        
    }
}
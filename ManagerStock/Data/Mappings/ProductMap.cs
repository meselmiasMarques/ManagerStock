using ManagerStock.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagerStock.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();

    
        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(50)
            .HasColumnName("name")
            .HasColumnType("varchar(50)");
        
        builder.Property(p => p.Description)
            .HasMaxLength(100)
            .HasColumnName("description")
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Price)
            .IsRequired(true)
            .HasColumnName("price")
            .HasColumnType("numeric(18,2)"); 

        builder.Property(p => p.StockAmout)
            .IsRequired(true)
            .HasColumnName("stockamount")
            .HasColumnType("integer")
            .HasDefaultValue(1);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("createdat")
            .HasColumnType("timestamp") 
            .HasColumnType("timestamp with time zone");

    }
}
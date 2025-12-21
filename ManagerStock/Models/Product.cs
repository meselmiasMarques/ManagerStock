namespace ManagerStock.Models;

public class Product
{
    public Product() { }
    
    
    public Product(
        string? name, 
        string? description, 
        decimal price, 
        int stockAmout)
    {
        Name = name;
        Description = description;
        Price = price;
        StockAmout = stockAmout;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get;  set; }
    public decimal Price { get; set; }
    public int StockAmout  { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    
}
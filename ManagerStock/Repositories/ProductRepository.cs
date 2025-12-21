using ManagerStock.Data;
using ManagerStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerStock.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
        => _context = context;

    public async Task<List<Product>> GetAllProductsAsync()
        => await _context.Products.ToListAsync();

    public async Task<Product> GetProductByIdAsync(int id)
        =>  await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddProductAsync(Product product)
    {
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }


    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    { 
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
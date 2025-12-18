using ManagerStock.Data;
using ManagerStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerStock.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
        => _context = context;

    public async Task<List<Products>> GetAllProductsAsync()
        => await _context.Products.ToListAsync();

    public async Task<Products> GetProductByIdAsync(int id)
        =>  await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddProductAsync(Products products)
    {
        await _context.Products.AddAsync(products);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateProductAsync(Products products)
    {
        _context.Products.Update(products);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Products products)
    { 
        _context.Products.Remove(products);
        await _context.SaveChangesAsync();
    }
}
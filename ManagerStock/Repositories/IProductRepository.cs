using ManagerStock.Models;

namespace ManagerStock.Repositories;

public interface IProductRepository
{
    Task<List<Products>> GetAllProductsAsync();
    Task<Products> GetProductByIdAsync(int id);
    Task AddProductAsync(Products products);
    Task UpdateProductAsync(Products products);
    Task DeleteProductAsync(Products products);
}
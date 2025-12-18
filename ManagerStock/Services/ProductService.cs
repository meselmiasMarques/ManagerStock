using ManagerStock.Data;
using ManagerStock.Models;
using ManagerStock.Repositories;
using ManagerStock.ViewModels.Product;
using ManagerStock.ViewModels.ResultViewModel;

namespace ManagerStock.Services;

public class ProductService
{
   private readonly IProductRepository _repository;

   public ProductService(IProductRepository repository)
      => _repository = repository;
   
   public async Task<Products> CreateAsync(EditorProductViewModel model)
   {
     if (model.Price < 0) 
         throw new ArgumentException("Preço inválido");
      
     if (model.StockAmout < 0) 
        throw new ArgumentException("Estoque inválido");
      
     var product = new Products(
        model.Name,
        model.Description,
        model.Price,
        model.StockAmout
     );
      
     await _repository.AddProductAsync(product);
     return product;
   }

   public async Task<List<Products>> GetAllAsync(
      decimal? minPrice,
      decimal? maxPrice,
      string? search 
      )
   {
      var products = await _repository.GetAllProductsAsync();

      if (!string.IsNullOrWhiteSpace(search))
         products = products.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();

      products = products.Where(p => p.Price >= minPrice).ToList();
      
      products = products.Where(p => p.Price <= maxPrice).ToList();
      
      
      return products;
   }
   
   public async Task<ResultViewModel<List<Products>>> GetAllProductWhenStockAsync()
   {
      var products = await _repository.GetAllProductsAsync();
     products = products.Where(p => p.StockAmout == 0).ToList();
     
      return new ResultViewModel<List<Products>>(products);
   }
   
   public async Task<ResultViewModel<Products>> GetByIdAsync(int id)
   {
      var product = await _repository.GetProductByIdAsync(id);
      if (product == null)
      {
         return new ResultViewModel<Products>("Produto não foi encontrado");
      }

      return new ResultViewModel<Products>(product);
   }
   
   public async Task<ResultViewModel<Products>> UpdateAsync(int id,EditorProductViewModel model)
   {
      if (model.Price < 0) 
         throw new ArgumentException("Preço inválido");
      
      if (model.StockAmout < 0) 
         throw new ArgumentException("Estoque inválido");

      var product = await _repository.GetProductByIdAsync(id);
      if (product == null)
         return new ResultViewModel<Products>("Produto não foi encontrado");
      
      product.Name = model.Name;
      product.Description = model.Description;
      product.Price = model.Price;
      product.StockAmout = model.StockAmout;
      
      await _repository.UpdateProductAsync(product);
      return new ResultViewModel<Products>(product);
   }
   
   public async Task<ResultViewModel<Products>> StockDown(int id, EditorStockViewModel model)
   {
      var product = await _repository.GetProductByIdAsync(id);
      
      if (product == null)
         return new ResultViewModel<Products>("Produto não foi encontrado");
      
      if (product.StockAmout < model.StockAmout)
         return new ResultViewModel<Products>("Não foi possível Atualizar o estoque do produto");
      
      product.StockAmout = (product.StockAmout - model.StockAmout);
      
      await _repository.UpdateProductAsync(product);
      return new ResultViewModel<Products>(product);
   }
   
   public async Task<ResultViewModel<Products>> RemoveAsync(int id)
   {
      
      var product = await _repository.GetProductByIdAsync(id);
      if (product == null)
         return new ResultViewModel<Products>("Produto não foi encontrado");
      await _repository.DeleteProductAsync(product);
      return new ResultViewModel<Products>(product);
   }
}
using Market.Api.Entities;

namespace Market.Api.service;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Guid id, Product product);
    Task<bool> DeleteProductAsync(Guid id);
}
using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Services.ProductsServices
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
    }
}
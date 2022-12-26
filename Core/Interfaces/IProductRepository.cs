using Core.Entities;
namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsnc(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}
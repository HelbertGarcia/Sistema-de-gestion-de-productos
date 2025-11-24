

using SGP.Services.DTO.Product;

namespace SGP.Services.Interfaces.Product
{
    public interface IProductService
    {
        public Task<ProductDto> AddProductAsync(AddProductDto addProductDto);
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        public Task<ProductDto?> GetProductByIdAsync(int id);
        public Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
    }
}

using SGP.Persistence.Context;
using SGP.Domain.Entities;
using SGP.Services.DTO.Product;
using SGP.Services.Interfaces.Product;
using Microsoft.EntityFrameworkCore;

namespace SGP.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductDto> AddProductAsync(AddProductDto addProductDto)
        {
            var product = new Product
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price
            };

            var productDto = new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };  

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products.Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToListAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            var productDto = product == null ? null : new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            return productDto;
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return null;

            product.Name = updateProductDto.Name ?? product.Name;
            product.Description = updateProductDto.Description ?? product.Description;
            product.Price = updateProductDto.Price;

            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}

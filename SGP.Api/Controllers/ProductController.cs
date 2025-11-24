using Microsoft.AspNetCore.Mvc;
using SGP.Services.DTO.Customer;
using SGP.Services.DTO.Product;
using SGP.Services.Interfaces.Product;

namespace SGP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] AddProductDto productDto)
        {
            try
            {
                var newProduct = await _productService.AddProductAsync(productDto);

                return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        // PUT: api/customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] UpdateProductDto productDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, productDto);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

    }
}

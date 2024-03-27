using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NowakowskiLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductResponseDTO> GetProducts([FromQuery] ProductFilterRequestDTO filter)
        {
            return _productService.GetProducts(filter);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductRequestDTO productDTO)
        {
            try
            {
                var addedProduct = _productService.AddProduct(productDTO);
                return CreatedAtAction(nameof(GetProductById), new
                {
                    id = addedProduct.ID
                }, addedProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}/Deactivate")]
        public IActionResult DeactivateProduct(int id)
        {
            try
            {
                _productService.DeactivateProduct(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}/Activate")]
        public IActionResult ActivateProduct(int id)
        {
            try
            {
                _productService.ActivateProduct(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
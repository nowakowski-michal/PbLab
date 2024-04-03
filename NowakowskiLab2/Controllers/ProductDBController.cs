using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NowakowskiLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDBController : ControllerBase
    {
        private readonly IProductServiceDB _productService;

        public ProductDBController(IProductServiceDB productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductRequestDTO productDTO)
        {
            var newProduct = _productService.AddProduct(productDTO);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ID }, newProduct);
        }

        [HttpPut("{id}/activate")]
        public IActionResult ActivateProduct(int id)
        {
            _productService.ActivateProduct(id);
            return Ok("Product activated successfully");
        }

        [HttpPut("{id}/deactivate")]
        public IActionResult DeactivateProduct(int id)
        {
            _productService.DeactivateProduct(id);
            return Ok("Product deactivated successfully");
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductFilterRequestDTO filter)
        {
            var products = _productService.GetProducts(filter);
            return Ok(products);
        }
    }
}


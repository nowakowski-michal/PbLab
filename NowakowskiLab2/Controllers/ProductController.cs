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

        [HttpPost]
        public ProductResponseDTO AddProduct([FromBody] ProductCreateRequestDTO productDTO)
        {
            return _productService.AddProduct(productDTO);
        }

        [HttpPut("{id}/Deactivate")]
        public void DeactivateProduct(int id)
        {
            _productService.DeactivateProduct(id);
        }

        [HttpPut("{id}/Activate")]
        public void ActivateProduct(int id)
        {
            _productService.ActivateProduct(id);
        }

    }
}

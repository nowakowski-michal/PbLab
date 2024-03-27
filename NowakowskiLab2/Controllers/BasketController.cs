using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NowakowskiLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public IActionResult AddToBasket(BasketItemRequestDTO basket)
        {
            try
            {
                _basketService.AddToBasket(basket);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ChangeNumberOfProducts(int id, int numberOfProducts)
        {
            try
            {
                _basketService.ChangeNumberOfProducts(id, numberOfProducts);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("generateorder/{userId}")]
        public IActionResult GenerateOrder(int userId)
        {
            try
            {
                var order = _basketService.GenerateOrder(userId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("pay/{userId}/{value}")]
        public IActionResult Pay(int userId, double value)
        {
            try
            {
                _basketService.Pay(userId, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveProductFromBasket(int id)
        {
            try
            {
                _basketService.RemoveProductFromBasket(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

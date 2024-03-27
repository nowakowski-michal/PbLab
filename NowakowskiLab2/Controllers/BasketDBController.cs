using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NowakowskiLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketDBController : ControllerBase
    {
        private readonly IBasketServiceDB _basketService;

        public BasketDBController(IBasketServiceDB basketService)
        {
            _basketService = basketService;
        }
        [HttpPost("add")]
        public IActionResult AddToBasket([FromBody] BasketItemRequestDTO basketItem)
        {
            _basketService.AddToBasket(basketItem);
            return Ok("Product added to basket successfully");
        }

        [HttpPut("{id}/changeamount/{amount}")]
        public IActionResult ChangeNumberOfProducts(int id, int amount)
        {
            _basketService.ChangeNumberOfProducts(id, amount);
            return Ok("Number of products changed successfully");
        }

        [HttpPost("generateorder/{userId}")]
        public IActionResult GenerateOrder(int userId)
        {
            var order = _basketService.GenerateOrder(userId);
            return Ok(order);
        }

        [HttpPut("pay/{userId}/{value}")]
        public IActionResult Pay(int userId, double value)
        {
            _basketService.Pay(userId, value);
            return Ok("Order paid successfully");
        }

        [HttpDelete("{id}/remove")]
        public IActionResult RemoveProductFromBasket(int id)
        {
            _basketService.RemoveProductFromBasket(id);
            return Ok("Product removed from basket successfully");
        }
    }
}


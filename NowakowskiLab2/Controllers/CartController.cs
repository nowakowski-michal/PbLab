using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NowakowskiLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public void AddToCart(int userID, BasketItemRequestDTO itemDTO)
        {
            _cartService.AddToCart(userID, itemDTO);
        }

        [HttpPut("update")]
        public void UpdateCartItem(int userID, BasketItemRequestDTO itemDTO)
        {
            _cartService.UpdateCartItem(userID, itemDTO);
        }

        [HttpDelete("remove")]
        public void RemoveFromCart(int userID, int productID)
        {
            _cartService.RemoveFromCart(userID, productID);
        }

        [HttpPost("generate-order")]
        public OrderResponseDTO GenerateOrder(int userID, OrderRequestDTO orderDTO)
        {
            return _cartService.GenerateOrder(userID, orderDTO);
        }

        [HttpPost("pay-order")]
        public void PayForOrder(int orderID, double amountPaid)
        {
            _cartService.PayForOrder(orderID, amountPaid);
        }
    }

}

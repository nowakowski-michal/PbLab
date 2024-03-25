using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface ICartService
    {
        void AddToCart(int userID, BasketItemRequestDTO itemDTO);
        void UpdateCartItem(int userID, BasketItemRequestDTO itemDTO);
        void RemoveFromCart(int userID, int productID);
        OrderResponseDTO GenerateOrder(int userID, OrderRequestDTO orderDTO);
        void PayForOrder(int orderID, double amountPaid);
    }
}

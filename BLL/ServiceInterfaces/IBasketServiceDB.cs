using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IBasketServiceDB
    {
        void AddToBasket(BasketItemRequestDTO basket);
        void ChangeNumberOfProducts(int id, int numberOfProducts);
        void RemoveProductFromBasket(int id);
        OrderResponseDTO GenerateOrder(int userId);
        void Pay(int userId, double value);
    }
}

using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class BasketService : IBasketService
    {
        private readonly WebshopContext _dbContext;

        public BasketService(WebshopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddToBasket(BasketItemRequestDTO basket)
        {

            Product? product = _dbContext.Products.FirstOrDefault(p => p.ID == basket.ProductID);
            if (product == null || !product.IsActive)
                return;

            BasketPosition basketTemp = new BasketPosition
            {
                ProductID = (int)basket.ProductID,
                UserID = (int)basket.UserID,
                Amount = basket.Amount,
                Price = basket.Price,
            };
            _dbContext.BasketPositions.Add(basketTemp);
            _dbContext.SaveChanges();
        }

        public void ChangeNumberOfProducts(int id, int numberOfProducts)
        {
            if (numberOfProducts <= 0)
                return;
            BasketPosition? basket = _dbContext.BasketPositions.FirstOrDefault(p => p.ID == id);
            if (basket == null) return;
            basket.Amount = numberOfProducts;
            _dbContext.SaveChanges();
        }

        public OrderResponseDTO GenerateOrder(int userId)
        {

            BasketPosition? basket = _dbContext.BasketPositions.FirstOrDefault(b => b.UserID == userId);
            if (basket == null)
                return null;

            Order order = new Order
            {
                UserID = basket.UserID,
                Date = DateTime.Now,
                isPayed = false,
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            OrderPosition orderPosition = new OrderPosition
            {
                ProductID = basket.ProductID,
                Amount = basket.Amount,
                Price = basket.Price,
                OrderID = order.ID,
            };

            _dbContext.OrderPositions.Add(orderPosition);
            _dbContext.SaveChanges();
            return new OrderResponseDTO
            {
                ID = order.ID,
                Date = order.Date,
                isPayed = false,
                UserID = basket.UserID,
            };


        }

        public void Pay(int userId, double value)
        {
            var order = _dbContext.Orders?.FirstOrDefault(x => x.UserID == userId);
            if (order != null && !order.isPayed)
            {
                var priceToPay = _dbContext.OrderPositions.Where(x => x.OrderID == order.ID).Select(x => x.Price).FirstOrDefault();
                var amount = _dbContext.OrderPositions.Where(x => x.OrderID == order.ID).Select(x => x.Amount).FirstOrDefault();
                priceToPay = priceToPay * amount;
                if (priceToPay == value)
                {
                    order.isPayed = true;
                    _dbContext.SaveChanges();
                }
                else throw new ArgumentException("Invalid price");
            }
        }

        public void RemoveProductFromBasket(int id)
        {
            BasketPosition? basket = _dbContext.BasketPositions.FirstOrDefault(b => b.ID == id);
            if (basket == null)
                return;
            _dbContext.BasketPositions.Remove(basket);
            _dbContext.SaveChanges();
        }


    }
}

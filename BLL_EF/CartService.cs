using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class CartService : ICartService
    {
        private readonly WebshopContext _dbContext;

        public CartService(WebshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddToCart(int userID, BasketItemRequestDTO itemDTO)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ID == itemDTO.ProductID);
            if (product == null)
                throw new ArgumentException("Product not found.");

            if (!product.IsActive)
                throw new InvalidOperationException("Cannot add inactive product to cart.");

            var basketPosition = _dbContext.BasketPositions.FirstOrDefault(bp => bp.UserID == userID && bp.ProductID == itemDTO.ProductID);
            if (basketPosition != null)
            {
                basketPosition.Amount += itemDTO.Amount;
            }
            else
            {
                basketPosition = new BasketPosition
                {
                    UserID = userID,
                    ProductID = itemDTO.ProductID,
                    Amount = itemDTO.Amount,
                    Price = product.Price
                };
                _dbContext.BasketPositions.Add(basketPosition);
            }
            _dbContext.SaveChanges();
        }

        public void UpdateCartItem(int userID, BasketItemRequestDTO itemDTO)
        {
            var basketPosition = _dbContext.BasketPositions.FirstOrDefault(bp => bp.UserID == userID && bp.ProductID == itemDTO.ProductID);
            if (basketPosition == null)
                throw new ArgumentException("Product not found in the cart.");

            if (itemDTO.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            basketPosition.Amount = itemDTO.Amount;
            _dbContext.SaveChanges();
        }

        public void RemoveFromCart(int userID, int productID)
        {
            var basketPosition = _dbContext.BasketPositions.FirstOrDefault(bp => bp.UserID == userID && bp.ProductID == productID);
            if (basketPosition == null)
                throw new ArgumentException("Product not found in the cart.");

            _dbContext.BasketPositions.Remove(basketPosition);
            _dbContext.SaveChanges();
        }

        public OrderResponseDTO GenerateOrder(int userID, OrderRequestDTO orderDTO)
        {
            var order = new Order
            {
                UserID = userID,
                Date = DateTime.Now,
                isPayed = false
            };

            foreach (var item in orderDTO.Items)
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.ID == item.ProductID);
                if (product == null)
                    throw new ArgumentException($"Product with ID {item.ProductID} not found.");

                if (!product.IsActive)
                    throw new InvalidOperationException($"Product with ID {item.ProductID} is inactive.");

                order.Positions.Add(new OrderPosition
                {
                    ProductID = item.ProductID,
                    Amount = item.Amount,
                    Price = product.Price
                });
            }

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return new OrderResponseDTO(order.ID, order.Positions.Sum(op => op.Price * op.Amount));
        }

        public void PayForOrder(int orderID, double amountPaid)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.ID == orderID);
            if (order == null)
                throw new ArgumentException("Order not found.");

            if (order.isPayed)
                throw new InvalidOperationException("Order has already been paid.");

            var orderTotalPrice = order.Positions.Sum(op => op.Price * op.Amount);
            if (amountPaid != orderTotalPrice)
                throw new ArgumentException("Paid amount does not match the total order price.");

            order.isPayed = true;
            _dbContext.SaveChanges();
        }
    }
}

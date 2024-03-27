using BLL.DTOModels;
using BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DAL;
using Model;

namespace BLL_DB
{
    public class BasketServiceDB : IBasketServiceDB
    {
        private readonly WebshopContext _dbContext;

        public BasketServiceDB(WebshopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddToBasket(BasketItemRequestDTO basket)
        {
            var parameters = new[]
            {
                new SqlParameter("@ProductID", basket.ProductID),
                new SqlParameter("@UserID", basket.UserID),
                new SqlParameter("@Amount", basket.Amount),
                new SqlParameter("@Price", basket.Price)
            };

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureAddProductToBasket @ProductID, @UserID, @Amount, @Price", parameters);
        }

        public void ChangeNumberOfProducts(int id, int numberOfProducts)
        {
            var basketPositionIdParam = new SqlParameter("@BasketPositionID", id);
            var newAmountParam = new SqlParameter("@NewAmount", numberOfProducts);

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureChangeAmount @BasketPositionID, @NewAmount",
                basketPositionIdParam, newAmountParam);
        }

        public OrderResponseDTO GenerateOrder(int userId)
        {
            var userIdParam = new SqlParameter("@UserID", userId);
            var orderDateParam = new SqlParameter("@OrderDate", DateTime.Now); // Możesz ustawić datę według własnych potrzeb

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureGenerateOrder @UserID, @OrderDate", userIdParam, orderDateParam);
            var order = _dbContext.Orders.OrderByDescending(o => o.Date).FirstOrDefault();
            return new OrderResponseDTO
            {
                ID = order.ID,
                Date = order.Date,
                isPayed = false,
                UserID = userId,
            };

        }

        public void Pay(int userId, double value)
        {
            var orderIdParam = new SqlParameter("@OrderID", userId);
            var paidAmountParam = new SqlParameter("@PaidAmount", value);

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedurePayOrder @OrderID, @PaidAmount",
                orderIdParam, paidAmountParam);

        }

        public void RemoveProductFromBasket(int id)
        {
            //wywolanie triggera
            _dbContext.Database.ExecuteSqlRaw("DELETE FROM BasketPositions WHERE ID = @Id", new SqlParameter("@Id", id));

            //wywolanie procedury:
            //var basketPositionIdParam = new SqlParameter("@BasketPositionID", id);
            // _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureRemoveProductFromBasket @BasketPositionID", basketPositionIdParam);
        }
    }
}

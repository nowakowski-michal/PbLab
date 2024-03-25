using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class OrderResponseDTO
    {
        public int OrderID { get; init; }
        public double TotalPrice { get; init; }

        public OrderResponseDTO(int orderID, double totalPrice)
        {
            OrderID = orderID;
            TotalPrice = totalPrice;
        }
    }
}

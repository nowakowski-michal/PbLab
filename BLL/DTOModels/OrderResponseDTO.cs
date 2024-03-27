using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class OrderResponseDTO
    {
        public int ID { get; init; }
        public int? UserID { get; init; }
        public DateTime Date { get; init; }
        public List<OrderPositionResponseDTO>? Positions { get; init; }
        public bool isPayed { get; init; }
        public double TotalPrice { get; init; }


    }
}

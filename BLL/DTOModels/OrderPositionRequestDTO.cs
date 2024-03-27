using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class OrderPositionRequestDTO
    {
        public int? OrderID { get; init; }
        public int Amount { get; init; }
        public double Price { get; init; }
        public int? ProductID { get; init; }
    }
}

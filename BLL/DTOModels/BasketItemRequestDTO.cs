using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.DTOModels
{
    public class BasketItemRequestDTO
    {
        public int? ProductID { get; set; }
        public int? UserID { get; init; }
        public int Amount { get; init; }
        public double Price { get; init; }
    }
}

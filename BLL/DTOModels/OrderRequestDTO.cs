using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class OrderRequestDTO
    {
        public int? UserID { get; init; }
        public DateTime Date { get; init; }
        public List<OrderPositionRequestDTO>? Positions { get; init; }
        public bool isPayed { get; set; }
    }
}

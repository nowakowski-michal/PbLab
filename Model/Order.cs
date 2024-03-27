using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
        public DateTime Date { get; set; }
        public List<OrderPosition>? Positions { get; set; }
        public bool isPayed { get; set; }
    }
}

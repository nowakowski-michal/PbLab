using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? GroupID { get; set; }
        [ForeignKey(nameof(GroupID))]
        public ProductGroup? Group { get; set; }
        public List<BasketPosition> BasketPositions { get; set; }
        public List<OrderPosition> OrderPositions { get; set; }

    }
}

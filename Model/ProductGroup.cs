using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID {  get; set; }
        [ForeignKey(nameof(ParentID))]
        public ProductGroup? Parent { get; set; }
        public List<ProductGroup> Childrens { get; set; }
        public List<Product> Products { get; set;}
    }
}

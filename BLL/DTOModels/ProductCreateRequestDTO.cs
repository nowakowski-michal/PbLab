using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class ProductCreateRequestDTO
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public int GroupID { get; init; }
    }
}

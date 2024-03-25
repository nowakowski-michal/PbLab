using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<BasketPosition> BasketPositions { get; set; }
    }
}

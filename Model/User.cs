using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum UserType
    {
        Admin,
        Casual
    }
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool isActive { get; set; }
        public int? GroupID { get; set; }
        [ForeignKey(nameof(GroupID))]
        public UserGroup? Group { get; set; }
        public List<Order> Orders { get; set; }
        public List<BasketPosition> BasketPositions { get; set; }
    }
}

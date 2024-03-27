using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class ProductResponseDTO
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public string Image { get; init; }
        public bool IsActive { get; init; }
        public int? GroupID { get; init; }
        public string? GroupName { get; init; }
        public List<BasketItemResponseDTO>? BasketPositions { get; init; }

        public ProductResponseDTO(int id, string name, double price, string groupName, bool isActive, int? gId,string image)
        {
            ID = id;
            Name = name;
            Price = price;
            GroupName = groupName;
            Image = image;
            IsActive = isActive;
            GroupID = gId;

        }
    }
}

using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IProductServiceDB
    {
        IEnumerable<ProductResponseDTO> GetProducts(ProductFilterRequestDTO filter);
        ProductResponseDTO GetProductById(int id);
        ProductResponseDTO AddProduct(ProductRequestDTO productDTO);
        void DeactivateProduct(int productId);
        void ActivateProduct(int productId);
    }
}

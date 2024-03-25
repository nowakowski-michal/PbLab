using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{

    public interface IProductService
    {
        IEnumerable<ProductResponseDTO> GetProducts(ProductFilterRequestDTO filter);
        ProductResponseDTO AddProduct(ProductCreateRequestDTO productDTO);
        void DeactivateProduct(int productID);
        void ActivateProduct(int productID);
        ProductResponseDTO GetProductById(int id);

    }
}

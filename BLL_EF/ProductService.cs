using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class ProductService: IProductService
    {
        private readonly WebshopContext _dbContext;

        public ProductService(WebshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductResponseDTO> GetProducts(ProductFilterRequestDTO filter)
        {
            var productsQuery = _dbContext.Products.AsQueryable();

            if (!filter.IncludeInactive)
                productsQuery = productsQuery.Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(filter.Name))
                productsQuery = productsQuery.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.GroupName))
                productsQuery = productsQuery.Where(p => p.Group != null && p.Group.Name.Contains(filter.GroupName));

            if (filter.GroupID.HasValue)
                productsQuery = productsQuery.Where(p => p.GroupID == filter.GroupID.Value);

            var products = productsQuery.Select(p => new ProductResponseDTO(p.ID, p.Name, p.Price, p.Group != null ? GetFullGroupName(p.Group) : "")).ToList();

            return products;
        }

        public ProductResponseDTO AddProduct(ProductCreateRequestDTO productDTO)
        {
            if (productDTO.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");
            
            var productGroup = _dbContext.ProductGroups.FirstOrDefault(g => g.ID == productDTO.GroupID);
            if (productGroup == null)
                throw new ArgumentException("Invalid product group ID.");
            
            var newProduct = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                GroupID = productDTO.GroupID,
                Image = productDTO.Name,
                IsActive = true // Nowy produkt jest domyślnie aktywny
            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return new ProductResponseDTO(newProduct.ID, newProduct.Name, newProduct.Price, GetFullGroupName(productGroup));
        }

        public void DeactivateProduct(int productID)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ID == productID);

            if (product == null)
                throw new ArgumentException("Product not found.");

            if (_dbContext.OrderPositions.Any(op => op.ProductID == productID) || _dbContext.BasketPositions.Any(bp => bp.ProductID == productID))
                throw new InvalidOperationException("Product cannot be deactivated or removed because it is associated with an unpaid order or a basket.");

            product.IsActive = false;
            _dbContext.SaveChanges();
        }

        public void ActivateProduct(int productID)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ID == productID);

            if (product == null)
                throw new ArgumentException("Product not found.");

            product.IsActive = true;
            _dbContext.SaveChanges();
        }

        private static string GetFullGroupName(ProductGroup group)
        {
            if (group == null)
                return "";

            string parentGroupName = GetFullGroupName(group.Parent);
            return parentGroupName != "" ? $"{parentGroupName} / {group.Name}" : group.Name;
        }

        public ProductResponseDTO GetProductById(int id) // Implementacja nowej metody
        {
            var product = GetProductById(id);
            if (product == null)
            {
                return null;
            }

            string groupName = product.GroupName != null ? product.GroupName : "";

            var productDTO = new ProductResponseDTO(product.ID, product.Name, product.Price, groupName);

            return productDTO;
        }
    }
}

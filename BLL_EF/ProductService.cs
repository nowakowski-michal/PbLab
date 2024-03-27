using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
    public class ProductService : IProductService
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

            // Sorting
            productsQuery = SortProducts(productsQuery, filter);

            var products = productsQuery.Select(p => new ProductResponseDTO(p.ID, p.Name, p.Price, p.Image,p.IsActive,p.GroupID, p.Group != null ? GetFullGroupName(p.Group) : "")).ToList();

            return products;
        }
        public ProductResponseDTO GetProductById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return null;
            }

            string groupName = product.Group != null ? GetFullGroupName(product.Group) : "";

            return new ProductResponseDTO(product.ID, product.Name, product.Price, product.Image, product.IsActive,product.GroupID ,groupName);
        }

        public ProductResponseDTO AddProduct(ProductRequestDTO productDTO)
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
                Image = productDTO.Image,
                IsActive = true 
            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return new ProductResponseDTO(newProduct.ID, newProduct.Name, newProduct.Price, newProduct.Image,newProduct.IsActive,newProduct.GroupID, GetFullGroupName(productGroup));
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

        // Helper method to sort products
        private IQueryable<Product> SortProducts(IQueryable<Product> query, ProductFilterRequestDTO filter)
        {
            if (filter.SortBy == "name")
                return filter.SortAsc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
            else if (filter.SortBy == "price")
                return filter.SortAsc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
            else if (filter.SortBy == "group")
                return filter.SortAsc ? query.OrderByDescending(p => p.Group.Name) : query.OrderBy(p => p.Group.Name);
            else
                return query; // Default sorting
        }
    }

}
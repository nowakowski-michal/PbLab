using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Data;

namespace BLL_DB
{
    public class ProductServiceDB : IProductServiceDB
    {
        private readonly WebshopContext _dbContext;

        public ProductServiceDB(WebshopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void ActivateProduct(int productId)
        {
            var parameter = new SqlParameter("@ProductID", productId);

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureActivateProduct @ProductID", parameter);
        }

        public ProductResponseDTO AddProduct(ProductRequestDTO productDTO)
        {
            var parameters = new[]
            {
            new SqlParameter("@ProductName", productDTO.Name),
            new SqlParameter("@Price", productDTO.Price),
            new SqlParameter("@Image", productDTO.Image),
            new SqlParameter("@GroupID", productDTO.GroupID)
        };

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureAddNewProduct @ProductName, @Price, @Image, @GroupID", parameters);

            var newProduct = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                GroupID = productDTO.GroupID,
                Image = productDTO.Image,
                IsActive = true
            };

            return new ProductResponseDTO(newProduct.ID, newProduct.Name, newProduct.Price, newProduct.Image, newProduct.IsActive, newProduct.GroupID, null);
        }

        public void DeactivateProduct(int productId)
        {
            var parameter = new SqlParameter("@ProductID", productId);

            _dbContext.Database.ExecuteSqlRaw("EXEC ProcedureDezactivateProduct @ProductID", parameter);

        }

        public ProductResponseDTO GetProductById(int id)
        {
            var parameter = new SqlParameter("@ProductID", id);

            var product = _dbContext.Products
                .FromSqlRaw("EXEC dbo.ProcedureGetProductByID @ProductID", parameter)
                .AsEnumerable() // Wywołujemy AsEnumerable() aby spowodować kompozycję zapytania po stronie klienta
                .Select(p => new ProductResponseDTO(p.ID, p.Name, p.Price, p.Image, p.IsActive,
                p.GroupID, null)).FirstOrDefault();

            return product;
        }

        public IEnumerable<ProductResponseDTO> GetProducts(ProductFilterRequestDTO filter)
        {
            var sortByParam = new SqlParameter("@SortOption", filter.SortBy);
            var sortAscParam = new SqlParameter("@SortDirection", SqlDbType.Bit);
            sortAscParam.Value = filter.SortAsc ? 1 : 0;
            var filterByNameParam = new SqlParameter("@FilterByName", filter.Name ?? (object)DBNull.Value);
            var filterByGroupNameParam = new SqlParameter("@FilterByGroupName", filter.GroupName ?? (object)DBNull.Value);
            var filterByGroupIDParam = new SqlParameter("@FilterByGroupID", filter.GroupID ?? (object)DBNull.Value);
            var includeInactiveParam = new SqlParameter("@IncludeInactive", filter.IncludeInactive);

            var products = _dbContext.Products
                .FromSqlRaw("EXEC ProcedureGetProduct @IncludeInactive, @SortOption, @SortDirection, @FilterByName, @FilterByGroupName, @FilterByGroupID",
                     new SqlParameter("@IncludeInactive", SqlDbType.Bit) { Value = filter.IncludeInactive ? 1 : 0 },
                    new SqlParameter("@SortOption", SqlDbType.NVarChar) { Value = filter.SortBy },
                    new SqlParameter("@SortDirection", SqlDbType.Bit) { Value = filter.SortAsc ? 1 : 0 },
                    new SqlParameter("@FilterByName", SqlDbType.NVarChar) { Value = (object)filter.Name ?? DBNull.Value },
                    new SqlParameter("@FilterByGroupName", SqlDbType.NVarChar) { Value = (object)filter.GroupName ?? DBNull.Value },
                    new SqlParameter("@FilterByGroupID", SqlDbType.Int) { Value = (object)filter.GroupID ?? DBNull.Value })

                .AsEnumerable() // Spowoduje kompozycję zapytania po stronie klienta
                .Select(p => new ProductResponseDTO(p.ID, p.Name, p.Price, p.Image, p.IsActive, p.GroupID, p.Group != null ? p.Group.Name : ""))
                .ToList();

            return products;
        }
    }
}

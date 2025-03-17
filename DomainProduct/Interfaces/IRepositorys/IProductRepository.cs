using EntitieProduct;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace DomainProduct.Interfaces.IRepositorys
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByCode(int codigo);

        Task<Product> GetLastProdutByCode();

        Task<Product> DeleteProduct(int codigo);

        Task<List<Product>> ListAllProductByPage(int pageNumber, int pageSize);

        Task<List<Product>> ListProductByDateManufactureWhitPagination(DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
    }
}

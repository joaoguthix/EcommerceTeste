using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DomainProduct.Views.ProductViews;

namespace DomainProduct.Interfaces.IServices
{
    public interface IProductService : IGenericService
    {
        Task<ProductView> GetByCode(int codigo);

        Task<string> DeleteProduct(int codigo);

        Task<List<ProductView>> ListAllProductByPage(int pageNumber, int pageSize);

        Task<List<ProductView>> ListProductByDateManufactureWhitPagination(DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
    }
}

using DomainProduct.Views.ProductViews;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DomainProduct.Interfaces.IServices
{
    public interface IGenericService
    {
        Task<ProductView> Add(ProductAddView entity);
        Task<string> Delete(int code);
        Task<ProductView> GetByCode(int codigo);
        Task<List<ProductView>> GetAll();
        Task<ProductView> Update(ProductUpdateView entity);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainProduct.Interfaces.IRepositorys
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task Update(T entity);
    }
}

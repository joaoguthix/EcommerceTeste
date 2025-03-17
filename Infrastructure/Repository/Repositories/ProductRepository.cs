using DomainProduct.Interfaces.IRepositorys;
using EntitieProduct;
using Infrastructure.Config;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class ProductRepository : RepositoryGeneric<Product>, IProductRepository
    {
        private readonly DbContextOptions<SqlLiteDbContext> _OptionsBuilder;

        public ProductRepository()
        {
            _OptionsBuilder = new DbContextOptions<SqlLiteDbContext>();
        }

        public async Task<Product> GetByCode(int code)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))

            {return await data.Product.FirstOrDefaultAsync(p => p.Codigo == code);
            }
        }

        public async Task<Product> GetLastProdutByCode()
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))

            {
                return await data.Product
                    .OrderByDescending(p => p.Codigo)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Product> DeleteProduct(int code)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                var product = await data.Product.FirstOrDefaultAsync(p => p.Codigo == code);

                if (product != null)
                {
                    product.Situacao = "Inativo";
                    await data.SaveChangesAsync();
                }

                return product;
            }
        }

        public async Task<List<Product>> ListAllProductByPage(int pageNumber, int pageSize)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                var products = await data.Product
                                    .OrderBy(p => p.Codigo)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
                return products;
            }
        }

        public async Task<List<Product>> ListProductByDateManufactureWhitPagination(DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
        {
            const string situacao = "Ativo";
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                var products = await data.Product
                                    .Where(p => p.DataFabricacao >= startDate && p.DataFabricacao <= endDate && p.Situacao == situacao)
                                    .OrderBy(p => p.DataFabricacao)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
                return products;
            }
        }
    }
}

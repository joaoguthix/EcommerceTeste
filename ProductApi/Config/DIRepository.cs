using Domain.Interfaces.IRepositorys;
using DomainProduct.Interfaces.IRepositorys;
using Infrastructure.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ProductApi.Config
{
    public class DIRepository
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            // Registra as dependências
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        }
    }
}

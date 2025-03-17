using Domain.Interfaces.IServices;
using Domain.Services;
using DomainProduct.Interfaces.IServices;
using DomainProduct.Services;

namespace ProductApi.Config
{
    public class DIServices
    {
        public void MapDependencies(IServiceCollection services)
        {
            // Mapeia as dependências relacionadas a NewOrder
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}

using AutoMapper;
using Domain.Views.OrderViews;
using DomainProduct.Views.ProductViews;
using EntitieProduct;

namespace ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductAddView, Product>();
                config.CreateMap<Product, ProductAddView>();
                config.CreateMap<ProductView, Product>();
                config.CreateMap<Product, ProductView>();
                config.CreateMap<ProductAddView, ProductView>();
                config.CreateMap<ProductView, ProductAddView>();
                config.CreateMap<Product, ProductUpdateView>();
                config.CreateMap<ProductUpdateView, Product>();
                config.CreateMap<Product, ProductUpdateView>();

                
                config.CreateMap<OrderAddView, Order>();
                config.CreateMap<Order, OrderAddView>();
                config.CreateMap<OrderView, Order>();
                config.CreateMap<Order, OrderView>();
                config.CreateMap<OrderUpdateView, OrderView>();
                config.CreateMap<OrderView, OrderUpdateView>();
                config.CreateMap<OrderUpdateView, Order>();
                config.CreateMap<OrderItemView, OrderItem>();
                config.CreateMap<OrderItem, OrderItemView>();



            });
            return mappingConfig;
        }
    }
}

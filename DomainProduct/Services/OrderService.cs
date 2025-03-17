using AutoMapper;
using Domain.Interfaces.IRepositorys;
using Domain.Interfaces.IServices;
using Domain.Utils.HttpStatusExceptionCustom;
using Domain.Views.OrderViews;
using DomainProduct.Interfaces.IRepositorys;
using EntitieProduct;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderView> Add(OrderAddView order)
        {
            var newOrder = _mapper.Map<Order>(order);

            foreach (var item in order.OrderItem)
            {
                var product = await _productRepository.GetByCode(item.ProductCode);
                if (product == null || product.Situacao != "Ativo")
                {
                    throw new HttpStatusExceptionCustom(StatusCodeEnum.NotFound, $"Produto {item.ProductCode} não encontrado ou inativo.");
                }

                product.Quantidade -= item.Quantity;
                await _productRepository.Update(product);
            }

            await _orderRepository.Add(newOrder);

            var mappingOrderItem = _mapper.Map<List<OrderItem>>(order.OrderItem);
            //await _orderItemRepository.AddRange(mappingOrderItem);

            var mapReturnOrder = _mapper.Map<OrderView>(newOrder);

            return mapReturnOrder;
        }

        public async Task<OrderView> Update(OrderUpdateView order)
        {
            var mapOrder = _mapper.Map<Order>(order);

            var getOrder = await _orderRepository.GetById(order.Id);

            if (getOrder == null)
            {
                throw new HttpStatusExceptionCustom(StatusCodeEnum.Conflict, "Falha ao Atualizar o Pedido (Pedido não encontrado).");
            }

            await _orderRepository.Update(mapOrder);

            var mapReturnOrder = _mapper.Map<OrderView>(mapOrder);

            return mapReturnOrder;
        }

        public async Task<List<OrderView>> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            var orderItens = await _orderItemRepository.GetAll();

            var returnMapOrders = _mapper.Map<List<OrderView>>(orders);

            returnMapOrders.ForEach(order =>
            {
                order.OrderItem = new List<OrderItemView>();
                orderItens.ForEach(orderItem =>
                {
                    if (orderItem.OrderId == order.Id)
                    {
                        order.OrderItem.Add(_mapper.Map<OrderItemView>(orderItem));
                    }
                });
            });

            return returnMapOrders;
        }
    }
}

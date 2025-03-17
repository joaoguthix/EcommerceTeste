using Domain.Interfaces.IServices;
using Domain.Utils.HttpStatusExceptionCustom;
using Domain.Views.OrderViews;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Produces("application/json")]
        [HttpPost("/api/AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] OrderAddView order)
        {
            try
            {
                var returnOrder = await _orderService.Add(order);
                return Created("Pedido Adicionado com sucesso!", returnOrder);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpPut("/api/UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateView order)
        {
            try
            {
                var returnOrder = await _orderService.Update(order);
                return Ok(returnOrder);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Produces("application/json")]
        [HttpGet("/api/ListAllOrders")]
        public async Task<IActionResult> ListAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAll();
                return Ok(orders);
            }
            catch (HttpStatusExceptionCustom ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase,IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService orderService)
        {
            this._OrderService = orderService;
        }
        [HttpPost("{UserName}")]
        public Task<OrderDTO> CreateOrderAsync(string UserName,/*[FromBody]*/ CreateOrderModel OrderModel)
        {
            return _OrderService.CreateOrderAsync(UserName, OrderModel);
        }
        [HttpGet("{id}")]
        public OrderDTO GetOrderById(int id)
        {
            return _OrderService.GetOrderById(id);
        }
        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _OrderService.GetUserOrders(UserName);
        }
    }
}
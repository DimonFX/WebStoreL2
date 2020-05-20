using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    /// <summary>
    /// Контроллер заказов
    /// </summary>
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase,IOrderService
    {
        private readonly IOrderService _OrderService;
        private readonly ILogger<OrdersApiController> _Logger;

        public OrdersApiController(IOrderService orderService, ILogger<OrdersApiController> Logger)
        {
            this._OrderService = orderService;
            _Logger = Logger;
        }
        /// <summary>
        /// Создание нового заказа для указанного пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя для которого оформляется заказ</param>
        /// <param name="OrderModel">Структура формируемого заказа</param>
        /// <returns>Структура сформированного заказа</returns>
        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrderAsync(string UserName,/*[FromBody]*/ CreateOrderModel OrderModel)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentException("Не указано имя пользователя");
            }
            if (string.IsNullOrEmpty(OrderModel.OrderViewModel.Address))
            {
                throw new ArgumentException("Не указан адрес доставки");
            }


            _Logger.LogInformation("Создается заказ для пользователя {0}",UserName);

            var order = await _OrderService.CreateOrderAsync(UserName, OrderModel);

            _Logger.LogInformation("Заказ для пользователя {0} создан успешно", UserName);

            return await _OrderService.CreateOrderAsync(UserName, OrderModel);
        }
        /// <summary>
        /// Получить заказ по его идентификатору
        /// </summary>
        /// <param name="id">идентификатор заказа</param>
        /// <returns>Заказ с указанным идентификатором, либо пустая ссылка, если заказ не найден</returns>
        [HttpGet("{id}")]
        public OrderDTO GetOrderById(int id)
        {
            return _OrderService.GetOrderById(id);
        }
        /// <summary>
        /// Получить все заказы пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Возвращает список заказов указанного ползователя</returns>
        [HttpGet("user/{UserName}")]
        public  IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _OrderService.GetUserOrders(UserName);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.ViewModels;
using WebStore.Domain.ViewModels.Orders;

namespace WebStore.Domain.DTO.Orders
{
    /// <summary>
    /// Структура оформляемого заказа
    /// </summary>
    public class CreateOrderModel
    {
        /// <summary>
        /// Информация о заказе
        /// </summary>
        public OrderViewModel OrderViewModel { get; set; }

        /// <summary>
        /// пункты закзаа
        /// </summary>
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}

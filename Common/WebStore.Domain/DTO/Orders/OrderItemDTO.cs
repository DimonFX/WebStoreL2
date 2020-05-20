﻿using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.DTO.Orders
{
    /// <summary>
    /// Описание пункта заказа
    /// </summary>
    public class OrderItemDTO:BaseEntity
    {
        /// <summary>
        /// Цена одной единицы
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// количество
        /// </summary>
        public int Quantity { get; set; }
    }
}

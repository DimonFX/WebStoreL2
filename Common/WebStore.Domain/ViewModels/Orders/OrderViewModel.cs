﻿using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Orders
{
    /// <summary>
    /// МОдель информации о заказе
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Номер телефона для связи
        /// </summary>
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        /// <summary>
        /// АДрес доставки
        /// </summary>
        [Required]
        public string Address { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebStore.Interfaces.Api
{
    public interface IValueServices
    {
        IEnumerable<string> Get();
        string Get(int id);
        /// <summary>
        /// Добавляет элемент внутрь себя
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Uri Post(string value);
        /// <summary>
        /// Обновляет элемент по id, возвращает статус код об успешности операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        HttpStatusCode Update(int id, string value);
        HttpStatusCode Delete(int id);

    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebStore.Interfaces.Api
{
    public interface IMyTestService
    {
        IEnumerable<double> Get();
        double Get(int id);
        /// <summary>
        /// Добавляет элемент внутрь себя
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Uri Post(double value);
        /// <summary>
        /// Обновляет элемент по id, возвращает статус код об успешности операции
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        HttpStatusCode Update(int id, double value);
        HttpStatusCode Delete(int id);
    }
}

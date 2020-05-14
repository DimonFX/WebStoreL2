using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void Add(Employee Employee);

        void Edit(int id, Employee Employee);

        bool Delete(int id);

        void SaveChanges();
    }
}

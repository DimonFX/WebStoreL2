using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient,IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration):base(configuration,WebAPI.Employees)
        {

        }

        public void Add(Employee Employee) => Post(_ServiceAddress, WebAPI.Employees);

        public bool Delete(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public void Edit(int id, Employee Employee) => Put($"{_ServiceAddress}/{id}", Employee);

        public IEnumerable<Employee> GetAll() => Get<List<Employee>>(_ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_ServiceAddress}/{id}");

        public void SaveChanges()
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase,IEmployeesData
    {
        private readonly IEmployeesData _employeesData;
        public EmployeesApiController(IEmployeesData employeesData) => _employeesData = employeesData;
        [HttpPost]
        public void Add([FromBody]Employee Employee)
        {
            _employeesData.Add(Employee);
        }
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _employeesData.Delete(id);
        }
        [HttpPut("{id}")]
        public void Edit(int id,[FromBody] Employee Employee)
        {
            _employeesData.Edit(id, Employee);
        }
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _employeesData.GetAll();
        }
        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return _employeesData.GetById(id);
        }
        [NonAction]
        public void SaveChanges()
        {
            _employeesData.SaveChanges();
        }
    }
}
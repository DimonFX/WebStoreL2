using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/mytest")]
    [ApiController]
    public class MyTestController : ControllerBase
    {
        private static readonly List<double> _MyTest = Enumerable.Range(1, 15).Select(i =>Convert.ToDouble(i+i*i)).ToList();
        [HttpGet]
        public ActionResult<IEnumerable<double>> Get() => _MyTest;
        [HttpGet("{id}")]
        public ActionResult<double> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _MyTest.Count)
                return NotFound();

            return _MyTest[id];
        }
        [HttpPost]
        public void Post(double value) => _MyTest.Add(value);
        [HttpPut("{id}")]
        public ActionResult Put(int id, double value)
        {
            if (id < 0 || id >= _MyTest.Count)
                return BadRequest();

            _MyTest[id] = value;

            return Ok();//200 статусный код
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _MyTest.Count)
                return NotFound();

            _MyTest.RemoveAt(id);
            return Ok();
        }
    }
}
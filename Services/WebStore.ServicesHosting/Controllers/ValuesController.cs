using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _Values = Enumerable.Range(1, 10).Select(i => $"Value {i}").ToList();

        //public ValuesController()
        //{

        //}
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => _Values;
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _Values.Count)
                return NotFound();

            return _Values[id];
        }
        [HttpPost]
        public void Post(string value) => _Values.Add(value);
        [HttpPut("{id}")]
        public ActionResult Put (int id, string value)
        {
            if (id < 0 || id >= _Values.Count)
                return BadRequest();

            _Values[id] = value;

            return Ok();//200 статусный код
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _Values.Count)
                return NotFound();

            _Values.RemoveAt(id);
            return Ok();
        }



    }
}
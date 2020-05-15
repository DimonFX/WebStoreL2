using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Clients.Values;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class WebAPITestController : Controller
    {
        private readonly IValueServices _ValueServices;

        public WebAPITestController(IValueServices ValueServices) => _ValueServices = ValueServices;

        public IActionResult Index() => View(_ValueServices.Get());
        //private readonly IMyTestService _myTestService;
        //public WebAPITestController(IMyTestService myTestService) => _myTestService = myTestService;

        public IActionResult IndexMyTest([FromServices] IMyTestService myTestService) { 
            return View(myTestService.Get());
        }
    }
}
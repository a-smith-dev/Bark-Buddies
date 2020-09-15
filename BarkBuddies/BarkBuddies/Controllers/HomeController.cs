using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BarkBuddies.Models;
using BarkBuddies.Services;

namespace BarkBuddies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAnimalsService _service;

        public HomeController(ILogger<HomeController> logger, IAnimalsService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }


        //public IActionResult Details()
        //{
        //    var details = _service.Get().Result;
        //    var model = details.Animals[0];
        //    return View(model);
        //}
        //public IActionResult DetailsList()
        //{
        //    var details = _service.Get().Result;
        //    var model = details.Animals.ToList();
        //    return View(model);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

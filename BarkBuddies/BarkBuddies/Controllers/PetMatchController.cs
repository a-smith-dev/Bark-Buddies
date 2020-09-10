using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarkBuddies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarkBuddies.Controllers
{
    public class PetMatchController : Controller
    {
        private readonly IAnimalsService _service;
        public PetMatchController(IAnimalsService service)
        {
            _service = service;
        }
        // GET: MatchController1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            var details = _service.Get().Result;
            var model = details.Animals[0];
            return View(model);
        }

        public IActionResult DetailsList()
        {
            var details = _service.Get().Result;
            var model = details.Animals.ToList();
            return View(model);
        }
    }
}

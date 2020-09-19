using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BarkBuddies.Models;
using BarkBuddies.Services;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BarkBuddies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAnimalsService _service;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IAnimalsService service, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            if(currentUser != null)
            {
                return RedirectToAction("IndexTuple", "UserProfile");
            }
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
        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

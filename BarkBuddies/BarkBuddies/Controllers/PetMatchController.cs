using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarkBuddies.Data;
using BarkBuddies.Data.Entities;
using BarkBuddies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarkBuddies.Controllers
{
    public class PetMatchController : Controller
    {
        private readonly IAnimalsService _service;
        private readonly AnimalContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PetMatchController(IAnimalsService service, AnimalContext context, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _context = context;
            _userManager = userManager;
        }
       
        public ActionResult Index()
        {
            return View();
        }


        public IActionResult Details()
        {
            var currentUser = GetCurrentUserAsync().Result;
            var details = _service.Get(_context.Pets.Where(x => x.Owner.Equals(currentUser)).ToListAsync().Result, GetProfileAsync(currentUser.Id).Result).Result;
            var model = details.Animals.ToList();
            return View(model);
        }

        private async Task<UserProfile> GetProfileAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.User.Id == userId);
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);
    }
}

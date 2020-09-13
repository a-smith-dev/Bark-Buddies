using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BarkBuddies.Data;
using BarkBuddies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarkBuddies.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly AnimalContext _context;

        //private readonly IAnimalsService _service;
        private readonly AspNetUserManager<UserProfile> _userManager;

        public UserProfileController(AnimalContext context/*, IAnimalsService service*/, AspNetUserManager<UserProfile> userManager)
        {
            _context = context;
            //_service = service;
            _userManager = userManager;
        }

        public UserProfile GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User;
            var user = _userManager.GetUserAsync(currentUser).Result;
            return user;
        }

        public async Task<IActionResult> Index()
        {
            var currentId = GetCurrentUser().Id;
            //return View(await _context.UserProfiles.ToListAsync());
            return View(await _context.UserProfiles.FindAsync(currentId));
        }
 
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Pets
                .FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZipCode,HasChildren,HasCats")] UserProfile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserProfileId,ZipCode,HasChildren,HasCats")] UserProfile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new {id});
            }
            return View(profile);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == string.Empty)
            {
                return NotFound();
            }

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(string id)
        {
            return _context.UserProfiles.Any(p => p.Id == id);
        }
    }
}

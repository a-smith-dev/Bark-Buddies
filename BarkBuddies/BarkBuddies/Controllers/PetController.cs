using System.Linq;
using System.Threading.Tasks;
using BarkBuddies.Data;
using BarkBuddies.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarkBuddies.Controllers
{

    public class PetController : Controller
    {
        private readonly AnimalContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PetController(AnimalContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = GetCurrentUserAsync().Result;
           
            return View(await _context.Pets.Where(x => x.Owner.Equals(currentUser)).ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public IActionResult Create()
        {
        
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,Gender,Size,Breed")] Pet pet)
        {
             if (ModelState.IsValid)
            {
                var currentUser = await GetCurrentUserAsync();
                var thisPet = pet;
                thisPet.Owner = currentUser;
                _context.Add(thisPet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,Name,Age,Gender,Size,Breed")] Pet pet)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId))
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
            return View(pet);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.PetId == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(p => p.PetId == id);
        }

        private async Task<UserProfile> GetProfileAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.User.Id == userId);
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);

        private bool ProfileExists(IdentityUser user) => _context.UserProfiles.Any(p => p.User.Id == user.Id);
    }        
}

using System.Linq;
using System.Threading.Tasks;
using BarkBuddies.Data;
using BarkBuddies.Data.Entities;
using BarkBuddies.Services;
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

        public IActionResult Search()
        {
            var currentUser = GetCurrentUserAsync().Result;
            var details = _service.Get(_context.Pets
                .Where(x => x.Owner.Equals(currentUser)).ToListAsync().Result, 
                GetProfileAsync(currentUser.Id).Result).Result;
            var model = details.Animals.ToList();
            return View(model);
        }

        // Save individual pet to database table PetMatch




        // List out all pets in PetMatch table that match the ID of the user logged in



        // Update PetMatch database table to confirm if all pets are still adoptable




        // Adopt animal from PetMatch table to Pets table (link the user's ID to this new animal)
        // // Troubleshoot: call the Pet controller, pass in the pet we want to adopt, delete the pet from this table




        private async Task<UserProfile> GetProfileAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.User.Id == userId);
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);
    }
}

using System;
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

        public async Task<IActionResult> Index()
        {
            var currentUser = GetCurrentUserAsync().Result;
            return View(await _context.PetMatch.Where(x => x.User.Equals(currentUser)).ToListAsync());
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

        public async Task<IActionResult> SavePet(string id)
        {
            var currentUser = GetCurrentUserAsync().Result;
            var pet = _service.Get(id).Result.Animal;
            if (pet != null && _context.PetMatch.Where(x => x.PetId == pet.PetId).Count() == 0)
            {
                var petAge = Age.baby;
                Enum.TryParse(pet.Age, true, out petAge);
                var petSize = Size.small;
                Enum.TryParse(pet.Size, true, out petSize);

                _context.Add(new PetMatch {
                    PetId = pet.PetId,
                    Name = pet.Name,
                    Age = petAge,
                    Gender = pet.Gender,
                    Size = petSize,
                    Breed = pet.Breed,
                    Status = "adoptable",
                    User = currentUser});
                await _context.SaveChangesAsync();
                ViewBag.Success = $"{pet.Name} was successfully saved to list!";
            }
            
            return RedirectToAction("Search");
        }


        // TODO - List out all pets in PetMatch table that match the ID of the user logged in



        // TODO - Update PetMatch database table to confirm if all pets are still adoptable




        // TODO - Adopt animal from PetMatch table to Pets table (link the user's ID to this new animal)
        // // Troubleshoot: call the Pet controller, pass in the pet we want to adopt, delete the pet from this table




        private async Task<UserProfile> GetProfileAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.User.Id == userId);
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);
    }
}

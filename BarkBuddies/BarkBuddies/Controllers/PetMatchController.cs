using System;
using System.Collections.Generic;
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
            var petMatchList = await _context.PetMatch.Where(x => x.User.Equals(currentUser)).ToListAsync();
            foreach (var pet in petMatchList)
            {
                if (pet.Status != "adoptable" && _service.Get(pet.PetId.ToString()).Result == null)
                {
                    using (var db = _context)
                    {
                        var result = db.PetMatch.SingleOrDefaultAsync(x => x.PetId == pet.PetId);
                        if (result != null)
                        {
                            result.Result.Status = "Not Adoptable";
                            await db.SaveChangesAsync();
                        }
                    }
                    TempData["NotAdoptable"] += $"{pet.Name} has been adopted and updated below. \n";
                }
            }
            return View(petMatchList);
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
                    Breed = pet.Breeds.Primary,
                    Status = "adoptable",
                    Url = pet.Url,
                    User = currentUser});
                await _context.SaveChangesAsync();
                ViewBag.Success = $"{pet.Name} was successfully saved to list!";
            }
            return RedirectToAction("Search");
        }

        public async Task<IActionResult> Adopt(string id)
        {
            var currentUser = GetCurrentUserAsync().Result;
            var pet = _service.Get(id).Result.Animal;
            if (pet != null)
            {
                var petAge = Age.baby;
                Enum.TryParse(pet.Age, true, out petAge);
                var petSize = Size.small;
                Enum.TryParse(pet.Size, true, out petSize);

                _context.Add(new Pet
                {
                    Name = pet.Name,
                    Age = petAge,
                    Gender = pet.Gender,
                    Size = petSize,
                    Breed = pet.Breed,
                    Owner = currentUser
                });
                await _context.SaveChangesAsync();
                ViewData["Adopted"] = $"Congratulations! You've adopted {pet.Name}!";

                var petMatchList = _context.PetMatch.Where(x => x.PetId == pet.PetId).ToListAsync().Result;
                if (petMatchList.Count() != 0)
                {
                    using (var db = _context)
                    {
                        for (int i = 0; i < petMatchList.Count; i++)
                        {
                            var result = db.PetMatch.SingleOrDefaultAsync(x => x.PetId == petMatchList[i].PetId);
                            if (result != null)
                            {
                                result.Result.Status = "Not Adoptable";
                            }
                        }
                        await db.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private async Task<UserProfile> GetProfileAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.User.Id == userId);
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);
    }
}

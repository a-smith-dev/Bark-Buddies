using System.Linq;
using System.Threading.Tasks;
using BarkBuddies.Data;
using BarkBuddies.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BarkBuddies.Models;
using System;
using System.Collections.Generic;

namespace BarkBuddies.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly AnimalContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserProfileController(AnimalContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            var result = await GetProfileAsync(currentUser.Id);
            return View(result);
        }

        public async Task<IActionResult> IndexTuple()
        {  
            var currentUser = await GetCurrentUserAsync();
            var result = await GetProfileAsync(currentUser.Id);
            var petList = await _context.Pets.Where(x => x.Owner.Equals(currentUser)).ToListAsync();
            var model = new Tuple<UserProfile, List<Pet>>(result, petList);
            return View(model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ZipCode,HasChildren,HasCats")] UserProfile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    var currentProfile = await GetProfileAsync(currentUser.Id);
                    
                    if (currentProfile != null)
                    {
                        currentProfile.ZipCode = profile.ZipCode;
                        currentProfile.HasCats = profile.HasCats;
                        currentProfile.HasChildren = profile.HasChildren;
                        _context.Update(currentProfile);
                    }
                    else
                    {
                        profile.User = currentUser;
                        _context.Add(profile);
                    }

                    await _context.SaveChangesAsync();
                    ViewBag.Success = "Successfully saved changes"; // add this to the Index view of UserProfile to show success
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.User))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index", "UserProfile");
            }

            return View(profile);
        }

        private async Task<UserProfile> GetProfileAsync(string userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.User.Id == userId);
        }

        private async Task<IdentityUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(User);

        private bool ProfileExists(IdentityUser user) => _context.UserProfiles.Any(p => p.User.Id == user.Id);
    }
}

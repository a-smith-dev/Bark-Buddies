using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarkBuddies.Data;
using BarkBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarkBuddies.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserController
        private readonly AnimalContext _context;

        public UserProfileController(AnimalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //add in code to filter to userID
            return View(await _context.UserProfiles.ToListAsync());
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
        public async Task<IActionResult> Edit(int id, [Bind("UserProfileId,ZipCode,HasChildren,HasCats")] UserProfile profile)
        {
            if (id != profile.UserProfileId)
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
                    if (!ProfileExists(profile.UserProfileId))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.UserProfiles
                .FirstOrDefaultAsync(m => m.UserProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.UserProfiles.Any(p => p.UserProfileId == id);
        }
    }
}

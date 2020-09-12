using BarkBuddies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarkBuddies.Data
{
    public class AnimalContext : IdentityDbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options)
           : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PetMatch> PetMatch { get; set; }
        
    }
}

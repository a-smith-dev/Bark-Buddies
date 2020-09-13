using BarkBuddies.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarkBuddies.Data
{
    public class AnimalContext : IdentityDbContext<UserProfile>
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

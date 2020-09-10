using BarkBuddies.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarkBuddies.Data
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options)
           : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PetMatch> PetMatch { get; set; }

    }
}

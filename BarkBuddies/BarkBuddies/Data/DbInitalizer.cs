using BarkBuddies.Models;
using System.Linq;

namespace BarkBuddies.Data
{
    public class DbInitalizer
    {
        public static void Initialize(AnimalContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Pets.Any())
            {
           //     context.Pets.Add(new Pet() { Name = "Sadie", Age = "adult", Gender = "Female", Size = "small", Breed = "mixed"});

            }
            context.SaveChanges();
        }
    }
}

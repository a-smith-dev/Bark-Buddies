using System.Linq;

namespace BarkBuddies.Data
{
    public class DbInitalizer
    {
        public static void Initialize(AnimalContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Animals.Any())
            {
                context.Animals.Add(new Animal() { Age = "3", Name = "Sadie", Gender = "Female" });

            }
            context.SaveChanges();
        }
    }
}

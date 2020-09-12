using BarkBuddies.Models;
using System.Linq;

namespace BarkBuddies.Data
{
    public class DbInitalizer
    {
        public static void Initialize(AnimalContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}

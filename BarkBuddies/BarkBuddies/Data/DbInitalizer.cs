using Microsoft.EntityFrameworkCore;

namespace BarkBuddies.Data
{
    public class DbInitalizer
    {
        public static void Initialize(AnimalContext context)
        {
            context.Database.Migrate();
            context.SaveChanges();
        }
    }
}

using System;
using System.Linq;
using FB.EventSourcing.Domain.UserAggregate;

namespace FB.EventSourcing.Persistence.EntityFrameworkCore
{
    public static class DataGenerator
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Users.Any())
                return;

            context.Users.Add(new User("Furkan", "Bozdag", "furkan@bozdag.dev", "Aa123.", new DateTime(1975, 04, 23)));
            context.SaveChanges();
        }
    }
}
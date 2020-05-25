using FB.EventSourcing.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace FB.EventSourcing.Persistence.EntityFrameworkCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
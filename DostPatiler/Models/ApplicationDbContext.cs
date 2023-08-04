using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DostPatiler.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Hayvan> Hayvanlar { get; set; }
        public object Hayvan { get; internal set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

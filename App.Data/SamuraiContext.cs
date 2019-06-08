using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class SamuraiContext : DbContext
    {

        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {

        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
    }
}

using App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace App.Data
{
    public class SamuraiContext : DbContext
    {
        public static readonly LoggerFactory MyConsoleLoggerFactory =
            new LoggerFactory(
                new[] {
                    new ConsoleLoggerProvider((category, level)
                        => 
                    category == DbLoggerCategory.Database.Command.Name &&   // Only SQL Commands
                    level == LogLevel.Information,  // Level of details
                        true)    
                });

        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=SamuraiAppData; Trusted_Connection=True");
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // In EF Core, a one-to-one relationship requires a reference navigation property at both sides.
            // Keep in mind, on one-to - one relationship(in this sample, Samuari: Secret Identity) is optional.That's, a Samuari could not have a SecretIdentity. 
            // If it's required, you'll need a handle on bussiness logic.
            // Use Fluent API to configure one-to-one relationships if entities do not follow the conventions

            // On Many-to-Many relationship, there are no default conventions available in Entity Framework Core 
            // It cannot automatically configure a many-to-many relationship. You must configure it using Fluent API.
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId });
        }
    }
}

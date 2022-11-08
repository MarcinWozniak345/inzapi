using EllipticCurve.Utils;
using Microsoft.EntityFrameworkCore;

namespace Inzynierka_API.Entities
{
    public class BazaDbContext : DbContext
    {
        public BazaDbContext(DbContextOptions<BazaDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<TimeTable> TimeTables { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User1)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User2)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);


        }


    }
}
using ArcheryTracker.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcheryTracker.Logic.Database
{
public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Round> Rounds { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Property Bindings
            modelBuilder.Entity<User>().HasIndex(u => u.Id).IsUnique();

            // Session Property Bindings
            modelBuilder.Entity<Session>()
                .HasIndex(s => s.Id)
                .IsUnique();

            modelBuilder.Entity<Session>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.UserId);

            // Round Property Bindings
            modelBuilder.Entity<Round>()
                .HasOne<Session>()
                .WithMany()
                .HasForeignKey(r => r.SessionId);
        }
    }
}
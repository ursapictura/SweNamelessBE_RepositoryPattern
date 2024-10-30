using Microsoft.EntityFrameworkCore;
using SweNamelessBE_RepositoryPattern.Data;
using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.Data
{
    public class TicketRepublicDbContext :DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public _TicketRepublicDbContext(DbContextOptions<TicketRepublicDbContext> context) : base(context)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(EventData.Events);
            modelBuilder.Entity<Venue>().HasData(VenueData.Venues);
            modelBuilder.Entity<RSVP>().HasData(RSVPData.RSVPs);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.RSVP)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Events)
                .WithOne(e => e.Venue)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
}

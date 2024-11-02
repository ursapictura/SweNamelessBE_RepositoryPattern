using Microsoft.EntityFrameworkCore;
using SweNamelessBE_RepositoryPattern.Data;
using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.DTOs;


namespace SweNamelessBE_RepositoryPattern.Repositories
{
    public class TicketRepublicVenueRepository : ITicketRepublicVenueRepository
    {
        private readonly TicketRepublicDbContext _context;

        public TicketRepublicVenueRepository(TicketRepublicDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venue>> GetVenuesAsync()
        {
            return await _context.Venues
                .Include(v => v.Events)
                .OrderBy(venue => venue.Name)
                .ToListAsync();
        }

        public async Task<List<Venue>> GetVenuesByUserAsync(string uid)
        {
            return await _context.Venues
                .Include(v => v.Events)
                .OrderBy(venue => venue.Name)
                .Where(venue => venue.Uid == uid)
                .ToListAsync();
        }

        public async Task<Venue> GetVenueByIdAsync(int id)
        {
            Venue venue = await _context.Venues
                            .Include(venue => venue.Events)
                            .SingleOrDefaultAsync(venue => venue.Id == id);

            if (venue == null)
            {
                return null;
            }
            return venue;
        }

        public async Task<Venue> PostVenueAsync(Venue newVenue)
        {
            Venue venue = new()
            {
                Name = newVenue.Name,
                Address = newVenue.Address,
                City = newVenue.City,
                State = newVenue.State,
                Uid = newVenue.Uid,
            };

            _context.Venues.Add(newVenue);
            await _context.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue> UpdateVenueAsync(int id, Venue venue)
        {
            Venue venueToUpdate = await _context.Venues.SingleOrDefaultAsync(venue => venue.Id == id);

            if (venueToUpdate == null)
            {
                return null;
            }

            venueToUpdate.Name = venue.Name;
            venueToUpdate.Address = venue.Address;
            venueToUpdate.City = venue.City;
            venueToUpdate.State = venue.State;
            venueToUpdate.Uid = venue.Uid;

            await _context.SaveChangesAsync();
            return venue;
        }

        public async Task<Venue> DeleteVenueAsync(int id)
        {
            Venue venue = await _context.Venues.SingleOrDefaultAsync(venue => venue.Id == id);

            if (venue == null)
            {
                return null;
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return venue;
        }
    }
}

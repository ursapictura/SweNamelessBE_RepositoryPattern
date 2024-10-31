using Microsoft.EntityFrameworkCore;
using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.Data;
using SweNamelessBE_RepositoryPattern.DTOs;

namespace SweNamelessBE_RepositoryPattern.Repositories
{
    public class TicketRepublicEventRepository : ITicketRepublicEventRepository
    {
        private readonly TicketRepublicDbContext _context;

        public TicketRepublicEventRepository(TicketRepublicDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _context.Events
                    .Include(e => e.Venue)
                    .OrderBy(e => e.Date)
                    .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByUserAsync(string uid)
        {
            return await _context.Events
                    .Include(e => e.RSVP)
                    .Include(e => e.Venue)
                    .Where(e => e.Uid == uid)
                    .OrderBy(e => e.Date)
                    .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.RSVP)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Event> PostEventAsync(CreateEventDTO eventDTO)
        {
            var newEvent = new Event
            {
                Date = eventDTO.Date,
                Artist = eventDTO.Artist,
                VenueId = eventDTO.VenueId,
                ImageUrl = eventDTO.ImageUrl,
                TicketUrl = eventDTO.TicketUrl,
                TicketPrice = eventDTO.TicketPrice,
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<Event> UpdateEventAsync(int id, UpdateEventDTO eventDTO)
        {
            var eventToUpdate = await _context.Events.Include(b => b.Venue).FirstOrDefaultAsync(b => b.Id == id);
            
            if (eventToUpdate == null)
            {
                return null;
            }

            eventToUpdate.Artist = eventDTO.Artist ?? eventToUpdate.Artist;
            eventToUpdate.VenueId = eventDTO.VenueId != 0 ? eventDTO.VenueId : eventToUpdate.VenueId;
            eventToUpdate.TicketUrl = eventDTO.TicketUrl ?? eventToUpdate.TicketUrl;
            eventToUpdate.TicketPrice = eventDTO.TicketPrice == 0 ? eventDTO.TicketPrice : eventToUpdate.TicketPrice;
            eventToUpdate.ImageUrl = eventDTO.ImageUrl ?? eventToUpdate.ImageUrl;

            if (eventToUpdate.Date != null) // Assuming Date is a nullable DateTime
            {
                eventToUpdate.Date = eventDTO.Date;
            }


            await _context.SaveChangesAsync();
            return eventToUpdate;
        }

        public async Task<Event> DeleteEventAsync(int id)
        {
            var deleteEvent = await _context.Events.FirstOrDefaultAsync(b => b.Id == id);

            if (deleteEvent == null)
            {
                return null;
            }

            _context.Events.Remove(deleteEvent);
            await _context.SaveChangesAsync();
            return deleteEvent;
        }
    }
}

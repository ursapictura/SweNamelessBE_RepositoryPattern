using Microsoft.EntityFrameworkCore;
using SweNamelessBE_RepositoryPattern.Data;
using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.Repositories
{
    public class TicketRepublicRSVPRepository : ITicketRepublicRSVPRepository
    {
        private readonly TicketRepublicDbContext _context;

        public TicketRepublicRSVPRepository(TicketRepublicDbContext context)
        {
            _context = context;
        }

        public async Task<List<RSVP>> GetRSVPsAsync(string uid)
        {
            var rsvp = await _context.RSVPs
                       .Where(rsvp => rsvp.Uid == uid)
                       .OrderBy(rsvp => rsvp.Event.Date)
                       .Include(rsvp => rsvp.Event)
                       .ToListAsync();

            if (rsvp == null)
            {
                return null;
            }

            return rsvp;
        }

        public async Task<RSVP> GetSingleRSVPAsync(string uid, int eventId)
        {
            var rsvp = await _context.RSVPs.SingleOrDefaultAsync(rsvp => rsvp.Uid == uid && rsvp.EventId == eventId);
            return rsvp;
        }

        public async Task<RSVP> PostRSVPAsync(RSVP rsvp)
        {
            RSVP newRsvp = new()
            {
                Id = rsvp.Id,
                EventId = rsvp.EventId,
                Uid = rsvp.Uid,
            };

            _context.RSVPs.Add(newRsvp);
            await _context.SaveChangesAsync();
            return newRsvp;
        }

        public async Task<RSVP> DeleteRSVPAsync(string uid, int eventId)
        {
            RSVP rsvp = await _context.RSVPs.SingleOrDefaultAsync(rsvp => rsvp.Uid == uid && rsvp.EventId == eventId);

            if (rsvp == null)
            {
                return null;
            }

            _context.RSVPs.Remove(rsvp);
            await _context.SaveChangesAsync();
            return rsvp;
        }
    }
}

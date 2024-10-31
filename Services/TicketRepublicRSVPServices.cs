using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.DTOs;

namespace SweNamelessBE_RepositoryPattern.API.Services
{
    public class TicketRepublicRSVPService : ITicketRepublicRSVPService
    {
        private readonly ITicketRepublicRSVPRepository _ticketRepublicRSVPRepository;
        public TicketRepublicRSVPService(ITicketRepublicRSVPRepository ticketRepublicRSVPRepository)
        {
            _ticketRepublicRSVPRepository = ticketRepublicRSVPRepository;
        }

        public async Task<List<RSVP>> GetRSVPsAsync(string uid)
        {
            return await _ticketRepublicRSVPRepository.GetRSVPsAsync(uid);
        }

        public async Task<RSVP> PostRSVPAsync(RSVP rsvp)
        {
            return await _ticketRepublicRSVPRepository.PostRSVPAsync(rsvp);
        }
        public async Task<RSVP> DeleteRSVPAsync(int id)
        {
            return await _ticketRepublicRSVPRepository.DeleteRSVPAsync(id);
        }
    }
}

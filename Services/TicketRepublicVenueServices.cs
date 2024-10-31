using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.DTOs;
using SweNamelessBE_RepositoryPattern.Repositories;

namespace SweNamelessBE_RepositoryPattern.Services
{
    public class TicketRepublicVenueService : ITicketRepublicVenueService
    {
        private readonly ITicketRepublicVenuesRepository _venueServicesRepo;

        public TicketRepublicVenueService(ITicketRepublicVenuesRepository venueServicesRepo)
        {
            _venueServicesRepo = venueServicesRepo;
        }

        public async Task<List<Venue>> GetVenuesAsync()
        {
            return await _venueServicesRepo.GetVenuesAsync();
        }

        public async Task<Venue> GetVenueByIdAsync(int id)
        {
            return await _venueServicesRepo.GetVenueByIdAsync(id);
        }


        public async Task<List<Venue>> GetVenuesByUserAsync(string uid)
        {
            return await _venueServicesRepo.GetVenuesByUserAsync(uid);
        }

        public async Task<Venue> PostVenueAsync(Venue venue)
        {
            return await _venueServicesRepo.PostVenueAsync(venue);
        }

        public async Task<Venue> UpdateVenueAsync(int id, Venue newVenue)
        {
            return await _venueServicesRepo.UpdateVenueAsync(id, newVenue);
        }
        public async Task<Venue> DeleteVenueAsync(int id)
        {
            return await _venueServicesRepo.DeleteVenueAsync(id);
        }
    }
}

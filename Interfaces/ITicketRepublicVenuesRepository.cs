﻿using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.Interfaces
{
    public interface ITicketRepublicVenuesRepository
    {
        Task<List<Venue>> GetVenuesAsync();
        Task<List<Venue>> GetVenuesByUserAsync(string uid);
        Task<Venue> GetVenueByIdAsync(int id);
        Task<Venue> PostVenueAsync(Venue venue);
        Task<Venue> UpdateVenueAsync(int id, Venue newVenue);
        Task<Venue> DeleteVenueAsync(int id);
    }
}

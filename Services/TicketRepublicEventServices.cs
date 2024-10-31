using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.DTOs;

namespace SweNamelessBE_RepositoryPattern.Services
{
    public class TicketRepublicEventService : ITicketRepublicEventService
    {
        private readonly ITicketRepublicEventRepository _eventServicesRepo;

        public TicketRepublicEventService(ITicketRepublicEventRepository eventServicesRepo)
        {
            _eventServicesRepo = eventServicesRepo;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _eventServicesRepo.GetEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventServicesRepo.GetEventByIdAsync(id);
        }


        public async Task<List<Event>> GetEventsByUserAsync(string uid)
        {
            return await _eventServicesRepo.GetEventsByUserAsync(uid);
        }

        public async Task<Event> PostEventAsync(CreateEventDTO eventDTO)
        {
            return await _eventServicesRepo.PostEventAsync(eventDTO);
        }

        public async Task<Event> UpdateEventAsync(int id, UpdateEventDTO eventDTO)
        {
            return await _eventServicesRepo.UpdateEventAsync(id, eventDTO);

        }
        public async Task<Event> DeleteEventAsync(int id)
        {
            return await _eventServicesRepo.DeleteEventAsync(id);
        }
    }
}


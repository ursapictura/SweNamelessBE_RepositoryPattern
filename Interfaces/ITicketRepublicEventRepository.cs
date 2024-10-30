using SweNamelessBE_RepositoryPattern.DTOs;
using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.Interfaces
{
    public interface ITicketRepublicEventRepository
    {
        Task<List<Event>> GetEventsAsync();
        Task<List<Event>> GetEventsByUserAsync(string uid);
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> PostEventAsync(CreateEventDTO eventDTO);
        Task<Event> UpdateEventAsync(int id, UpdateEventDTO eventDTO);
        Task<Event> DeleteEventAsync(int id);
    }
}

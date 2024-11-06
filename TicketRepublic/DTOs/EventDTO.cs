using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.DTOs
{
    public class EventDTO
    {
        public DateTime Date { get; set; }
        public string? Artist { get; set; }
        public int VenueId { get; set; }

        public string? Uid { get; set; }
        public string? TicketUrl { get; set; }

        public decimal TicketPrice { get; set; }
    }
}

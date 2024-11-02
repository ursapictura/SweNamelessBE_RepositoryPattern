namespace SweNamelessBE_RepositoryPattern.Models
{
    public class RSVP
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string? Uid { get; set; }
        public Event? Event { get; set; }
    }
}

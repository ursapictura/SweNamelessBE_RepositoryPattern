﻿using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.DTOs
{
    public class CreateEventDTO
    {
        public string? Artist { get; set; }
        public int VenueId { get; set; }
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }
        public string? TicketUrl { get; set; }

        public string? Uid { get; set; }

        public decimal TicketPrice { get; set; }
    }
}

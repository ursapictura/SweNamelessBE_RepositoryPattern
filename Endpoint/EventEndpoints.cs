using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace SweNamelessBE_RepositoryPattern.Endpoint
{
    public static class EventEndpoints
    {
        public static void MapEventEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/events").WithTags(nameof(Event));

            group.MapGet("/", async (ITicketRepublicEventService eventService) =>
            {
                return await eventService.GetEventsAsync();
            })
                .WithName("GetEvents")
                .WithOpenApi()
                .Produces<List<Event>>(StatusCodes.Status200OK);

            group.MapGet("/{id}", async (ITicketRepublicEventService eventService, int id) =>
            {
                var occasion = await eventService.GetEventByIdAsync(id);
                return Results.Ok(occasion);
            })
                .WithName("GetEventById")
                .WithOpenApi()
                .Produces<Event>(StatusCodes.Status200OK);

            group.MapGet("/users/{uid}", async (ITicketRepublicEventService eventService, string uid) =>
            {
                var occasion = await eventService.GetEventsByUserAsync(uid);
                return Results.Ok(occasion);
            })
                .WithName("GetEventByUid")
                .WithOpenApi()
                .Produces<List<RSVP>>(StatusCodes.Status200OK);

            group.MapPost("/", async(ITicketRepublicEventService eventService, CreateEventDTO eventDTO) =>
            {
                var occasion = await eventService.PostEventAsync(eventDTO);
                return Results.Created($"/events/{occasion.Id}", occasion);
            })
                .WithName("PostEvent")
                .WithOpenApi()
                .Produces<Event>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id}", async (ITicketRepublicEventService eventService, int id, UpdateEventDTO eventDTO) =>
            {
                var occasion = await eventService.UpdateEventAsync(id, eventDTO);
                return Results.Ok(occasion);
            })
                .WithName("UpdateEvent")
                .WithOpenApi()
                .Produces<Event>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent);

            group.MapDelete("/{id}", async (ITicketRepublicEventService eventService, int id) =>
            {
                var occasion = await eventService.DeleteEventAsync(id);
                return Results.NoContent();
            })
                
                .WithName("DeleteEvent")
                .WithOpenApi()
                .Produces<RSVP>(StatusCodes.Status204NoContent);

        }  
       
    }
}

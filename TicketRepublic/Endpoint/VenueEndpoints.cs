using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;
using System;

namespace SweNamelessBE_RepositoryPattern.Endpoint
{
    public static class VenueEndpoints
    {
        public static void MapVenueEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/venues").WithTags(nameof(Venue));

            group.MapGet("/", async (ITicketRepublicVenueService venueService) =>
            {
                return await venueService.GetVenuesAsync();
            })
                .WithName("GetVenues")
                .WithOpenApi()
                .Produces<List<Venue>>(StatusCodes.Status200OK);

            group.MapGet("/users/{uid}", async (ITicketRepublicVenueService venueService, string uid) =>
            {
                var venue = await venueService.GetVenuesByUserAsync(uid);
                return Results.Ok(venue);
            })
                .WithName("Get Venues By User")
                .WithOpenApi()
                .Produces<List<Venue>>(StatusCodes.Status200OK);

            group.MapGet("/{id}", async (ITicketRepublicVenueService venueService, int id) =>
            {
                var venue = await venueService.GetVenueByIdAsync(id);
                return Results.Ok(venue);
            })
                .WithName("Get Venues By Id")
                .WithOpenApi()
                .Produces<Venue>(StatusCodes.Status200OK);

            group.MapPost("/", async (ITicketRepublicVenueService venueService, Venue venue) =>
            {
                var newVenue = await venueService.PostVenueAsync(venue);
                return Results.Created($"/venues/{venue.Id}", newVenue);
            })
                .WithName("Post Venue")
                .WithOpenApi()
                .Produces<Venue>(StatusCodes.Status201Created);

            group.MapPut("/{id}", async (ITicketRepublicVenueService venueService, int id, Venue venue) =>
            {
                var updatedVenue = await venueService.UpdateVenueAsync(id, venue);
                return Results.Ok(venue);
            })
                .WithName("Update Venue")
                .WithOpenApi()
                .Produces(StatusCodes.Status204NoContent);

            group.MapDelete("/{id}", async (ITicketRepublicVenueService venueService, int id) =>
            {
                var venue = await venueService.DeleteVenueAsync(id);
                return Results.NoContent();
            })
                .WithName("Delete Venue")
                .WithOpenApi()
                .Produces(StatusCodes.Status204NoContent);
        }
    }
    

}
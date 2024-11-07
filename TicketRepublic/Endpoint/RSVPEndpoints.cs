using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Models;

namespace SweNamelessBE_RepositoryPattern.Endpoint
{
    public static class RSVPEndpoints
    {
        public static void MapRSVPEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/rsvps").WithTags(nameof(RSVP));
            group.MapGet("/{uid}", async (ITicketRepublicRSVPService rsvpService, string uid) =>
            {
                var rsvp = await rsvpService.GetRSVPsAsync(uid);
                return Results.Ok(rsvp);
            })
                .WithName("GetRSVPByUid")
                .WithOpenApi()
                .Produces<List<RSVP>>(StatusCodes.Status200OK);

            group.MapGet("/{uid}/{eventId}", async (ITicketRepublicRSVPService rsvpService, string uid, int eventId) =>
            {
                var rsvp = await rsvpService.GetSingleRSVPAsync(uid, eventId);

                if (rsvp ==  null)
                {
                    return Results.Ok(true);
                }
                return Results.Ok(false);
            })
                .WithName("GetSingleRSVP")
                .WithOpenApi()
                .Produces<RSVP>(StatusCodes.Status200OK);

            group.MapPost("/", async (ITicketRepublicRSVPService rsvpService, RSVP rsvp) =>
            {
                var newRsvp = await rsvpService.PostRSVPAsync(rsvp);
                return Results.Created($"/rsvps/{rsvp.Id}", newRsvp);

            })
                .WithName("PostRSVP")
                .WithOpenApi()
                .Produces<RSVP>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{uid}/{eventId}", async (ITicketRepublicRSVPService rsvpService, string uid, int eventId) =>
            {
                var rsvp = await rsvpService.DeleteRSVPAsync(uid, eventId);
                return Results.NoContent();
            })
                .WithName("DeleteRSVP")
                .WithOpenApi()
                .Produces<RSVP>(StatusCodes.Status204NoContent);
        }
    }
}

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using SweNamelessBE_RepositoryPattern.Data;
using SweNamelessBE_RepositoryPattern.Endpoint;
using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Repositories;
using SweNamelessBE_RepositoryPattern.Services;



var builder = WebApplication.CreateBuilder(args);
// allows passing datetimes without time zone data
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<TicketRepublicDbContext>(builder.Configuration["TicketRepublicDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });


builder.Services.AddScoped<ITicketRepublicEventService, TicketRepublicEventService>();
builder.Services.AddScoped<ITicketRepublicEventRepository, TicketRepublicEventRepository>();
builder.Services.AddScoped<ITicketRepublicVenueService, TicketRepublicVenueService>();
builder.Services.AddScoped<ITicketRepublicVenueRepository, TicketRepublicVenueRepository>();
builder.Services.AddScoped<ITicketRepublicRSVPService, TicketRepublicRSVPService>();
builder.Services.AddScoped<ITicketRepublicRSVPRepository, TicketRepublicRSVPRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapEventEndpoints();
app.MapRSVPEndpoints();
app.Run();

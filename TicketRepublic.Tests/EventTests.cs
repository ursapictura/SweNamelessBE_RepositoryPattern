using Moq;
using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.DTOs;
using SweNamelessBE_RepositoryPattern.Services;
using Microsoft.Extensions.Logging;

namespace TicketRepublic.Tests
{
    public class EventTests
    {
        private readonly Mock<ITicketRepublicEventRepository> _mockTicketRepublicEventRepository;
        private readonly ITicketRepublicEventService _eventService;

        public EventTests()
        {
            _mockTicketRepublicEventRepository = new Mock<ITicketRepublicEventRepository>();
            _eventService = new TicketRepublicEventService(_mockTicketRepublicEventRepository.Object);

        }

        [Fact]
        public async Task GetEventAsync_WhenCalled_ReturnsEventsAsync()
        {
            var events = new List<Event>
            {
                new Event { Id = 1 },
                new Event { Id = 2 }

            };

            _mockTicketRepublicEventRepository.Setup(x => x.GetEventsAsync()).ReturnsAsync(events);

            var result = await _eventService.GetEventsAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
        [Fact]

        public async Task GetEventsByUserAsync_ShouldReturnUserEvents_WhenUserEventsExist()
        {
            var firstUserId = "jkfskdkskdd";
            var secondUserId = "bndkkieiiee";

            var eventItem = new List<Event>
            {
                new Event { Id = 1, Uid = firstUserId },
                new Event { Id = 2, Uid = secondUserId },
                new Event { Id = 3, Uid = firstUserId }
            };

            _mockTicketRepublicEventRepository.Setup(v => v.GetEventsByUserAsync(firstUserId)).ReturnsAsync(eventItem.Where(v => v.Uid == firstUserId).ToList());
            _mockTicketRepublicEventRepository.Setup(v => v.GetEventsByUserAsync(secondUserId)).ReturnsAsync(eventItem.Where(v => v.Uid == secondUserId).ToList());
            
            var firstUserResult = await _eventService.GetEventsByUserAsync(firstUserId);
            var secondUserResult = await _eventService.GetEventsByUserAsync(secondUserId);

            Assert.NotNull(firstUserResult);
            Assert.Equal(2, firstUserResult.Count);
            Assert.NotNull(secondUserResult);
            Assert.Single(secondUserResult);

            Assert.All(firstUserResult, eventItem => Assert.Equal(firstUserId, eventItem.Uid));
            Assert.All(secondUserResult, eventItem => Assert.Equal(secondUserId, eventItem.Uid));

        }
        [Fact]
        public async Task GetEventByIdAsync_WhenCalledWithValidId_ReturnsEventsAsync()
        {
            var eventId = 1;
            var eventItem = new Event { Id = eventId };

            _mockTicketRepublicEventRepository.Setup(x => x.GetEventByIdAsync(eventId)).ReturnsAsync(eventItem);

            var result = await _eventService.GetEventByIdAsync(eventId);

            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
        }

        [Fact]

        public async Task CreateEventAsync_WhenCalled_ReturnNewEventAsync()
        {
            var eventDTO = new CreateEventDTO
            {
                Date = DateTime.Now,
                Artist = "string",
                VenueId = 301,
                TicketUrl = "string",
                TicketPrice = 65.65m,
                ImageUrl = "string",
            };

            var eventItem = new Event
            {
                Date = eventDTO.Date,
                Artist = eventDTO.Artist,
                VenueId = eventDTO.VenueId,
                TicketUrl = eventDTO.TicketUrl,
                TicketPrice = eventDTO.TicketPrice,
                ImageUrl = eventDTO.ImageUrl,
            };
            _mockTicketRepublicEventRepository.Setup(x => x.PostEventAsync(eventDTO)).ReturnsAsync(eventItem);

            var result = await _eventService.PostEventAsync(eventDTO);
            Assert.NotNull(result);
            Assert.Equal(eventDTO.Date, result.Date);
            Assert.Equal(eventDTO.Artist, result.Artist);
            Assert.Equal(eventDTO.VenueId, result.VenueId);
            Assert.Equal(eventDTO.TicketUrl, result.TicketUrl);
            Assert.Equal(eventDTO.TicketPrice, result.TicketPrice);
            Assert.Equal(eventDTO.ImageUrl, result.ImageUrl);
        }

        [Fact]

        public async Task UpdateEventAsync_WhenCalled_ReturnUpdateEventAsync()
        {
            int eventId = 1;

            var eventItem = new Event
            {
                Date = DateTime.Now,
                Artist = "string",
                VenueId = 301,
                TicketUrl = "string",
                TicketPrice = 65.65m,
                ImageUrl = "string",

            };

            var editEventDTO = new UpdateEventDTO
            {
                Artist = "new string",
                TicketUrl = "newest string",
                Date = DateTime.Now,
                TicketPrice = 45.25m,

            };

            var updatedEvent = new Event
            {
                Artist = editEventDTO.Artist,
                TicketUrl = editEventDTO.TicketUrl,
                Date = editEventDTO.Date,
                TicketPrice = editEventDTO.TicketPrice,
            };

            _mockTicketRepublicEventRepository.Setup(x => x.GetEventByIdAsync(eventId)).ReturnsAsync(eventItem);
            _mockTicketRepublicEventRepository.Setup(x => x.UpdateEventAsync(eventId, editEventDTO)).ReturnsAsync(updatedEvent);

            var result = await _eventService.UpdateEventAsync(eventId, editEventDTO);

            Assert.NotNull(result);
            Assert.Equal(editEventDTO.Artist, result.Artist);
            Assert.Equal(editEventDTO.TicketUrl, result.TicketUrl);
            Assert.Equal(editEventDTO.Date, result.Date);
            Assert.Equal(editEventDTO.TicketPrice, result.TicketPrice);

        }

        [Fact]
        public async Task DeleteEventAsync_WhenCalledWithValidId_DeletesEventAsync()
        {
            
            var eventId = 1;

            _mockTicketRepublicEventRepository.Setup(x => x.DeleteEventAsync(eventId));

            
            await _eventService.DeleteEventAsync(eventId);

            
            _mockTicketRepublicEventRepository.Verify(x => x.DeleteEventAsync(eventId), Times.Once);
        }
    }
}
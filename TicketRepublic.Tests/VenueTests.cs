using SweNamelessBE_RepositoryPattern.Interfaces;
using SweNamelessBE_RepositoryPattern.Services;
using SweNamelessBE_RepositoryPattern.Models;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TicketRepublic.Tests
{
    public class VenueServiceTests
    {
        private readonly ITicketRepublicVenueService _venueService;

        private readonly Mock<ITicketRepublicVenueRepository> _mockVenueRepository;

        public VenueServiceTests()
        {
            _mockVenueRepository = new Mock<ITicketRepublicVenueRepository >();
            _venueService = new TicketRepublicVenueService((_mockVenueRepository.Object));
        }

        [Fact]
        public async Task GetVenuesAsync_WhenCalled_ReturnsVenuesAsync()
        {
            var venues = new List<Venue>
            {
                new Venue { Id = 1, Name = "Boogie Nights", Address = "1234 Alphabet City", City = "New York", Uid = "asdfhdajklfhudj" },
                new Venue { Id = 2, Name = "Studio 54", Address = "336 Lower Manhattan", City = "New York", Uid = "asdfhdajklfhudj" },
            };
            _mockVenueRepository.Setup(v => v.GetVenuesAsync()).ReturnsAsync(venues);

            var result = await _venueService.GetVenuesAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetVenuesByUserAsync_ShouldReturnUserVenues_WhenUserVenuesExist()
        {
            var firstUserId = "asdfhdajklfhudj";
            var secondUserId = "dsfjksdhfgjdhg";

            var venues = new List<Venue>
            {
                new Venue { Id = 1, Name = "Boogie Nights", Address = "1234 Alphabet City", City = "New York", Uid = firstUserId },
                new Venue { Id = 2, Name = "Studio 54", Address = "336 Lower Manhattan", City = "New York", Uid = firstUserId },
                new Venue { Id = 3, Name = "Bluebird Cafe", Address = "593 Bluebird Lane", City = "Chicago", State = "Illinois", Uid = secondUserId  }
            };

            _mockVenueRepository.Setup(v => v.GetVenuesByUserAsync(firstUserId)).ReturnsAsync(venues.Where(v => v.Uid == firstUserId).ToList());
            _mockVenueRepository.Setup(v => v.GetVenuesByUserAsync(secondUserId)).ReturnsAsync(venues.Where(v => v.Uid == secondUserId).ToList());

            var firstUserResult = await _venueService.GetVenuesByUserAsync(firstUserId);
            var secondUserResult = await _venueService.GetVenuesByUserAsync(secondUserId);

            Assert.NotNull(firstUserResult);
            Assert.Equal(2, firstUserResult.Count);
            Assert.NotNull(secondUserResult);
            Assert.Single(secondUserResult);

            // Assert that the venues for the first user are not the same as the venues for the second user
            Assert.All(firstUserResult, venue => Assert.Equal(firstUserId, venue.Uid));
            Assert.All(secondUserResult, venue => Assert.Equal(secondUserId, venue.Uid));
        }

        [Fact]
        public async Task GetVenueDetailsAsync_ShouldReturnVenue_WhenVenueExistsAsync()
        {
            var venueId = 1;

            var expectedVenue = new Venue { Id = venueId, Name = "Bluebird Cafe", Address = "593 Bluebird Lane", City = "Chicago", State = "Illinois", Uid = "dsfjksdhfgjdhg" };

            _mockVenueRepository.Setup(v => v.GetVenueByIdAsync(venueId)).ReturnsAsync(expectedVenue);

            var actualVenue = await _venueService.GetVenueByIdAsync(venueId);

            Assert.Equal(expectedVenue, actualVenue);
        }

        [Fact]
        public async Task CreateVenueAsync_WhenCalled_ReturnNewVenueAsync()
        {
            var newVenue = new Venue
            {
                Id = 1,
                Name = "Boogie Nights",
                Address = "1234 Alphabet City",
                City = "New York",
                Uid = "asdfhdajklfhudj"
            };

            _mockVenueRepository.Setup(v => v.PostVenueAsync(newVenue)).ReturnsAsync(newVenue);
            var result = await _venueService.PostVenueAsync(newVenue);

            Assert.NotNull(result);
            Assert.Equal(newVenue.Id, result.Id);
            Assert.Equal(newVenue.Name, result.Name);
            Assert.Equal(newVenue.Address, result.Address);
            Assert.Equal(newVenue.City, result.City);
            Assert.Equal(newVenue.State, result.State);
            Assert.Equal(newVenue.Uid, result.Uid);
        }

        [Fact]
        public async Task updateVenueAsync_WhenCalled_ReturnUpdateShowAsync()
        {
            int venueId = 1;

            var venue = new Venue
            {
                Id = venueId,
                Name = "Bluebird Cafe",
                Address = "593 Bluebird Lane",
                City = "Chicago",
                State = "Illinois",
                Uid = "dsfjksdhfgjdhg"
            };

            var venueUpdate = new Venue
            {
                Name = "Boogie Nights",
                Address = "1234 Alphabet City",
                City = "New York",
                Uid = "asdfhdajklfhudj"
            };

            _mockVenueRepository.Setup(v => v.GetVenueByIdAsync(venueId)).ReturnsAsync(venue);
            _mockVenueRepository.Setup(v => v.UpdateVenueAsync(venueId, venueUpdate)).ReturnsAsync(venueUpdate);

            var result = await _venueService.UpdateVenueAsync(venueId, venueUpdate);

            Assert.NotNull(result);
            Assert.Equal(venueUpdate.Id, result.Id);
            Assert.Equal(venueUpdate.Name, result.Name);
            Assert.Equal(venueUpdate.Address, result.Address);
            Assert.Equal(venueUpdate.City, result.City);
            Assert.Equal(venueUpdate.Uid, result.Uid);
        }

        [Fact]
        public async Task DeleteVenueAsync_WhenCalled_ReturnNoContent()
        {
            int venueId = 1;

            var venue = new Venue
            {
                Id = venueId,
                Name = "Bluebird Cafe",
                Address = "593 Bluebird Lane",
                City = "Chicago",
                State = "Illinois",
                Uid = "dsfjksdhfgjdhg"
            };

            _mockVenueRepository.Setup(v => v.GetVenueByIdAsync(venueId)).ReturnsAsync(venue);
            _mockVenueRepository.Setup(v => v.DeleteVenueAsync(venueId)).ReturnsAsync(venue);

            var result = await _venueService.DeleteVenueAsync(venueId);

            Assert.NotNull(result);
            _mockVenueRepository.Verify(v => v.DeleteVenueAsync(venueId), Times.Once);

        }
    }
}
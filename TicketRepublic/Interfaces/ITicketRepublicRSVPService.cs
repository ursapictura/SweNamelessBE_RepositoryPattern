using SweNamelessBE_RepositoryPattern.Models;
using SweNamelessBE_RepositoryPattern.Data;

namespace SweNamelessBE_RepositoryPattern.Interfaces
{
    public interface ITicketRepublicRSVPService
    {
        Task<List<RSVP>> GetRSVPsAsync(string uid);
        Task<RSVP> GetSingleRSVPAsync(string uid, int eventId);
        Task<RSVP> PostRSVPAsync(RSVP rsvp);
        Task<RSVP> DeleteRSVPAsync(string uid, int eventId);
    }
}

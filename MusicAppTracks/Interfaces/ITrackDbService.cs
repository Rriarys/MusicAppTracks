using MusicAppTracks.Models;

namespace MusicAppTracks.Interfaces
{
    public interface ITrackDbService
    {
        Task<List<Track>> GetAllAsync();
        Task<Track?> GetByIdAsync(int id);
        Task AddTrackAsync(Track track);
    }
}

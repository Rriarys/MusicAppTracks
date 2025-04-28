using Microsoft.EntityFrameworkCore;
using MusicAppTracks.Data;
using MusicAppTracks.Interfaces;
using MusicAppTracks.Models;

namespace MusicAppTracks.Services.Database
{
    public class TrackDbService : ITrackDbService
    {
        private readonly AppDbContext _context;

        public TrackDbService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Track>> GetAllAsync()
        {
            return await _context.Tracks.ToListAsync();
        }

        public async Task<Track?> GetByIdAsync(int id)
        {
            return await _context.Tracks.FindAsync(id);
        }

        public async Task AddTrackAsync(Track track)
        {
            track.CreatedAt = track.CreatedAt.ToUniversalTime();

            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();
        }
    }
}

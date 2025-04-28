using Microsoft.EntityFrameworkCore;
using MusicAppTracks.Models;

namespace MusicAppTracks.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Track> Tracks { get; set; } = default!;  // Таблица треков
    }
}

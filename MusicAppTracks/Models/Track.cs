namespace MusicAppTracks.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;      // Название трека
        public string Artist { get; set; } = default!;     // Исполнитель
        public string Album { get; set; } = default!;      // Альбом (новое поле)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Дата создания трека
        public string TrackUrl { get; set; } = default!;   // URL аудиофайла
        public string CoverUrl { get; set; } = default!;   // URL изображения обложки
    }
}

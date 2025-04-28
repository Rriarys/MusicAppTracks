using System.ComponentModel.DataAnnotations;

namespace MusicAppTracks.Models
{
    public class TrackViewModel
    {
        [Required]
        public string Title { get; set; } = default!;       // Название трека (для создания)
        [Required]
        public string Artist { get; set; } = default!;      // Исполнитель (для создания)
        [Required]
        public string Album { get; set; } = default!;       // Альбом (новое поле)
        [Required]
        public IFormFile TrackFile { get; set; } = default!;// Файл аудио (загружаемый)
        [Required]
        public IFormFile CoverFile { get; set; } = default!;// Файл обложки (загружаемый)
    }
}

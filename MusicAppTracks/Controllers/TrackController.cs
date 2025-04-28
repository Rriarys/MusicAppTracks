using Microsoft.AspNetCore.Mvc;
using MusicAppTracks.Interfaces;
using MusicAppTracks.Models;

namespace MusicAppTracks.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackDbService _trackDbService;
        private readonly IBlobStorageService _blobStorageService;

        public TrackController(ITrackDbService trackDbService, IBlobStorageService blobStorageService)
        {
            _trackDbService = trackDbService;
            _blobStorageService = blobStorageService;
        }

        // GET: Отображение списка треков
        public async Task<IActionResult> Index()
        {
            var tracks = await _trackDbService.GetAllAsync();
            return View(tracks);
        }

        // GET: Форма создания нового трека
        public IActionResult Create()
        {
            return View();
        }

        // POST: Создание нового трека
        [HttpPost]
        public async Task<IActionResult> Create(TrackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Загрузка файлов в Blob Storage
            string trackUrl = await _blobStorageService.UploadTrackAsync(model.TrackFile);
            string coverUrl = await _blobStorageService.UploadCoverAsync(model.CoverFile);

            // Создание сущности Track и сохранение в БД
            var track = new Track
            {
                Title = model.Title,
                Artist = model.Artist,
                Album = model.Album,
                CreatedAt = DateTime.Now,
                TrackUrl = trackUrl,
                CoverUrl = coverUrl
            };

            await _trackDbService.AddTrackAsync(track);
            return RedirectToAction(nameof(Index));
        }
    }
}

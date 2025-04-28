using MusicAppTracks.Interfaces;
using Azure.Storage.Blobs;

namespace MusicAppTracks.Services.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _tracksContainer;
        private readonly BlobContainerClient _coversContainer;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            // Инициализация контейнеров Blob Storage (создаются, если не существуют)
            _tracksContainer = _blobServiceClient.GetBlobContainerClient("tracks");
            _tracksContainer.CreateIfNotExists();
            _coversContainer = _blobServiceClient.GetBlobContainerClient("covers");
            _coversContainer.CreateIfNotExists();
        }

        public async Task<string> UploadTrackAsync(IFormFile trackFile)
        {
            if (trackFile == null || trackFile.Length == 0)
                throw new ArgumentException("Файл трека не указан");

            // Генерируем уникальное имя файла
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(trackFile.FileName);
            var blobClient = _tracksContainer.GetBlobClient(fileName);

            using (var stream = trackFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            // Возвращаем URI загруженного файла
            return blobClient.Uri.ToString();
        }

        public async Task<string> UploadCoverAsync(IFormFile coverFile)
        {
            if (coverFile == null || coverFile.Length == 0)
                throw new ArgumentException("Файл обложки не указан");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(coverFile.FileName);
            var blobClient = _coversContainer.GetBlobClient(fileName);

            using (var stream = coverFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return blobClient.Uri.ToString();
        }
    }
}

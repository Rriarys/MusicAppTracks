namespace MusicAppTracks.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadTrackAsync(IFormFile trackFile);
        Task<string> UploadCoverAsync(IFormFile coverFile);
    }
}

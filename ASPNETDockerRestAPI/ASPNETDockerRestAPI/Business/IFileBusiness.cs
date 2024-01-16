using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface IFileBusiness
    {
        Task<byte[]> GetFileAsync(string filename);
        Task<FileDetailsDto?> SaveFile(IFormFile file);
        Task<List<FileDetailsDto>> SaveFiles(IEnumerable<IFormFile> files);
    }
}

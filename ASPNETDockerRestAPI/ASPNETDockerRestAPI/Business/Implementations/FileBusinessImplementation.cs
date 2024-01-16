using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class FileBusinessImplementation(IHttpContextAccessor context) : IFileBusiness
    {
        private readonly List<string> _supportedExtensions = [".pdf", ".jpg", ".png", ".jpeg"];
        private readonly string _basePath = Directory.GetCurrentDirectory() + "\\Upload\\";

        public async Task<byte[]> GetFileAsync(string filename)
        {
            var filePath = _basePath + filename;

            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<FileDetailsDto?> SaveFile(IFormFile file)
        {
            var fileDetail = new FileDetailsDto();
            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = context.HttpContext!.Request.Host;

            if (!_supportedExtensions.Contains(fileType.ToLower()) || file.Length < 1)
            {
                return await Task.FromResult<FileDetailsDto?>(null);
            }

            var docName = Path.GetFileName(file.FileName);
            var destination = Path.Combine(_basePath, "", docName);
            using var stream = new FileStream(destination, FileMode.Create);

            fileDetail.DocumentName = docName;
            fileDetail.DocumentType = fileType;
            fileDetail.DocumentUrl = Path.Combine(baseUrl + "/api/v1/file/" + fileDetail.DocumentName);

            await file.CopyToAsync(stream);

            return fileDetail;
        }

        public async Task<List<FileDetailsDto>> SaveFiles(IEnumerable<IFormFile> files)
        {
            var filesDetails = new List<FileDetailsDto>();

            foreach (var file in files)
            {
                var uploadedFile = await SaveFile(file);

                if (uploadedFile is null)
                {
                    continue;
                }

                filesDetails.Add(uploadedFile);
            }

            return filesDetails;
        }
    }
}

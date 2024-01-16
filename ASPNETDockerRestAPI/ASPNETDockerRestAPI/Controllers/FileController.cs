using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using ASPNETDockerRestAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/files")]
    public class FileController(IFileBusiness fileBusiness) : ControllerBase
    {
        [HttpPost("upload")]
        [ProducesResponseType(200, Type = typeof(FileDetailsDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            var detail = await fileBusiness.SaveFile(file);

            return Ok(detail);
        }

        [HttpPost("upload-multiple")]
        [ProducesResponseType(200, Type = typeof(List<FileDetailsDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
        {
            var details = await fileBusiness.SaveFiles(files);

            return Ok(details);
        }

        [HttpGet("download/{filename}")]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFile(string filename)
        {
            var buffer = await fileBusiness.GetFileAsync(filename);
            var extension = $"application/{Path.GetExtension(filename).Replace(".", "")}";

            if (buffer is null)
            {
                return NotFound();
            }

            return File(buffer, extension, filename);
        }
    }
}

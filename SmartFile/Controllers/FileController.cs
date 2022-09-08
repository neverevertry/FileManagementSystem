using Application.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Models.Entities;
using System.Net;

namespace SmartFile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _service;
        private readonly IUrlService _urlService;
        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Resource");

        public FileController(IFileService service, IUrlService urlService)
        {
            _service = service;
            _urlService = urlService;
        }

        [HttpPost ("load")]
        public async Task<IActionResult> LoadFile(IFormFile file)
        {
            if(file != null)
            {
                await _service.SaveFileAsync(file.FileName, AppDirectory, file.ContentType);

                var path = _service.GetFilePath(AppDirectory, file.FileName);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("files")]
        public async Task<List<FileModel>> GetFiles() => await _service.GetAllFilesAsync();

        [HttpGet("/download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _service.GetFileByIdAsync(id);
            // ?? В отдельный метод до ретурна
            var path = Path.Combine(AppDirectory, file.FilePath);

            var memory = new MemoryStream();

            using(var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var contentType = "Application/octet-stream";
            var fileName = Path.GetFileName(path);

            return File(memory, contentType, fileName);
        }

        [HttpGet("tempLink")]
        public async Task<string> CreateTemporaryLink(int id)
        {
            string randomLink = _urlService.GenerateUrl(Request.Host.ToString());
            await _service.SetUrlForCurrentEntityAsync(id, randomLink);

            return randomLink;
        }

        [HttpGet("download/tempLink/{url}")]
        public async Task<IActionResult> Download(string url)
        {
            string host = Request.Host.ToString();

            string fullPathUrl = "https://" + host + "/file/download/templink/" + url;
            var file = await _service.GetFileByUrlAsync(fullPathUrl);

            return await DownloadFile(file.id);
        }

        [HttpPost("files/load")]
        public async Task<IActionResult> LoadFiles(List<IFormFile> files)
        {
            List<FileModel> models = files.Select(x => new FileModel
            {
                Name = x.Name,
                FilePath = _service.GetFilePath(AppDirectory, x.Name),
                ContentType = x.ContentType,
                FileFormat = Path.GetExtension(x.Name)
            }).ToList();

            await _service.SaveArrayFilesAsync(models);

            return Ok();
        }
    }
}

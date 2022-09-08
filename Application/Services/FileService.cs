using Application.Interfaces;
using Models.Entities;
using Models.Interfaces;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<List<FileModel>> GetAllFilesAsync()
        {
            var files = await _fileRepository.GetAllFilesAsync();

            if (files != null)
                return files;

            throw new Exception("There is no file");
        }

        public async Task<FileModel> GetFileByIdAsync(int id)
        {
            var file = await _fileRepository.GetFileByIdAsync(id);

            if (file != null)
                return file;

            throw new Exception("File not found");
        }

        public async Task SaveFileAsync(string fileName, string AppDirectory, string contentType)
        {
            FileModel model = new FileModel
            {
                FilePath = GetFilePath(AppDirectory, fileName),
                Name = fileName,
                FileFormat = Path.GetExtension(fileName),
                ContentType = contentType
            };

            await _fileRepository.SaveFileAsync(model);
        }

        public string GetFilePath(string AppDirectory, string fileName)
        {
            if (!Directory.Exists(AppDirectory))
                Directory.CreateDirectory(AppDirectory);

            return Path.Combine(AppDirectory, fileName);
        }

        public async Task UpdateAsync(FileModel file) => await _fileRepository.UpdateAsync(file);

        public async Task<FileModel> GetFileByUrlAsync(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
               var file =  await _fileRepository.GetFileByUrlAsync(url);

                if (file == null)
                    throw new Exception("File not found");

                return file;
            }

            throw new Exception("url is empty");
        }

        public async Task SaveArrayFilesAsync(List<FileModel> files)
        {
            if(files != null)
            {
                await _fileRepository.SaveArrayFilesAsync(files);
                return;
            }

            throw new Exception("empty files");
        }

        public async Task SetUrlForCurrentEntityAsync(int id, string randomLink)
        {
            var file = await _fileRepository.GetFileByIdAsync(id);
            file.Url = randomLink;
            await _fileRepository.UpdateAsync(file);
        }
    }
}

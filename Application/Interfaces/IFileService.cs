using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task SaveFileAsync(string fileName, string AppDirectory, string contentType);
        Task<List<FileModel>> GetAllFilesAsync();
        Task<FileModel> GetFileByIdAsync(int id);
        string GetFilePath(string AppDirectory, string fileName);
        Task UpdateAsync(FileModel file);
        Task<FileModel> GetFileByUrlAsync(string url);
        Task SaveArrayFilesAsync(List<FileModel> files);
        Task SetUrlForCurrentEntityAsync(int id, string randomLink);
    }
}

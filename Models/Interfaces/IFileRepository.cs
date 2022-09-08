using Models.Entities;

namespace Models.Interfaces
{
    public interface IFileRepository
    {
        Task SaveFileAsync(FileModel file);
        Task<List<FileModel>> GetAllFilesAsync();
        Task<FileModel> GetFileByIdAsync(int id);
        Task UpdateAsync(FileModel file);
        Task<FileModel> GetFileByUrlAsync(string url);
        Task SaveArrayFilesAsync(List<FileModel> files);
    }
}

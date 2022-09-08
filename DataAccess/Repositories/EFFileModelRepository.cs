using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Interfaces;

namespace DataAccess.Repositories
{
    public class EFFileModelRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;

        public EFFileModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveFileAsync(FileModel file)
        {
             await _context.AddAsync(file);
             await _context.SaveChangesAsync();
        }

        public async Task<List<FileModel>> GetAllFilesAsync() => await _context._fileModel.ToListAsync();

        public async Task<FileModel> GetFileByIdAsync(int id) => await _context._fileModel.FirstOrDefaultAsync(x => x.id == id);

        public async Task UpdateAsync(FileModel file)
        {
            _context.Update(file);
            await _context.SaveChangesAsync();
        }

        public async Task<FileModel> GetFileByUrlAsync(string url) => await _context._fileModel.FirstOrDefaultAsync(x => x.Url == url);

        public async Task SaveArrayFilesAsync(List<FileModel> files)
        {
            await _context.AddRangeAsync(files);
            await _context.SaveChangesAsync();
        }
    }
}

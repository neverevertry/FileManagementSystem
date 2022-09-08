using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FileModel> _fileModel { get; set; }
    }
}

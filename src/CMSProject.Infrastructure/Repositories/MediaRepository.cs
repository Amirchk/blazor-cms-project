using CMSProject.Core.Entities;
using CMSProject.Core.Interfaces;
using CMSProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMSProject.Infrastructure.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Media>> GetAllAsync()
        {
            return await _context.MediaFiles
                .OrderByDescending(m => m.UploadedDate)
                .ToListAsync();
        }

        public async Task<Media?> GetByIdAsync(int id)
        {
            return await _context.MediaFiles.FindAsync(id);
        }

        public async Task<Media> CreateAsync(Media media)
        {
            media.UploadedDate = DateTime.Now;
            _context.MediaFiles.Add(media);
            await _context.SaveChangesAsync();
            return media;
        }

        public async Task DeleteAsync(int id)
        {
            var media = await _context.MediaFiles.FindAsync(id);
            if (media != null)
            {
                _context.MediaFiles.Remove(media);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<long> GetTotalSizeAsync()
        {
            return await _context.MediaFiles.SumAsync(m => m.FileSize);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.MediaFiles.CountAsync();
        }
    }
}
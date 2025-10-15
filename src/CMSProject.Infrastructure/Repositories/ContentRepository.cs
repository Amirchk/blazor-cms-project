using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CMSProject.Core.Entities;
using CMSProject.Core.Interfaces;
using CMSProject.Infrastructure.Data;

namespace CMSProject.Infrastructure.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Content>> GetAllAsync()
        {
            return await _context.Contents
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Content?> GetByIdAsync(int id)
        {
            return await _context.Contents
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Content> CreateAsync(Content content)
        {
            content.CreatedDate = DateTime.Now;
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task UpdateAsync(Content content)
        {
            content.ModifiedDate = DateTime.Now;
            _context.Contents.Update(content);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content != null)
            {
                _context.Contents.Remove(content);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Content>> GetPublishedAsync()
        {
            return await _context.Contents
                .Include(c => c.Category)
                .Where(c => c.Status == ContentStatus.Published)
                .OrderByDescending(c => c.PublishedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Content>> SearchAsync(string query, ContentStatus? status = null)
        {
            var normalizedQuery = query.ToLowerInvariant();
            
            var baseQuery = _context.Contents
                .Include(c => c.Category)
                .AsQueryable();
                
            if (status.HasValue)
            {
                baseQuery = baseQuery.Where(c => c.Status == status);
            }
            
            return await Task.FromResult(new List<Content>());
                // .Where(c => c.Title.ToLower().Contains(normalizedQuery) || 
                //             c.Body.ToLower().Contains(normalizedQuery))
                // .OrderByDescending(c => c.PublishedDate)
                // .ToListAsync();
        }
    }
}
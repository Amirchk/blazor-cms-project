using CMSProject.Core.Entities;

namespace CMSProject.Core.Interfaces
{
    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetAllAsync();
        Task<IEnumerable<Content>> SearchAsync(string query, ContentStatus? status);
        Task<Content?> GetByIdAsync(int id);
        Task<Content> CreateAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(int id);
        Task<IEnumerable<Content>> GetPublishedAsync();
    }
}
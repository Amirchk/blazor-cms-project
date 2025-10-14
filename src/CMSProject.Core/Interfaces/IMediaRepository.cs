using CMSProject.Core.Entities;

namespace CMSProject.Core.Interfaces
{
    public interface IMediaRepository
    {
        Task<IEnumerable<Media>> GetAllAsync();
        Task<Media?> GetByIdAsync(int id);
        Task<Media> CreateAsync(Media media);
        Task DeleteAsync(int id);
        Task<long> GetTotalSizeAsync();
        Task<int> GetTotalCountAsync();
    }
}
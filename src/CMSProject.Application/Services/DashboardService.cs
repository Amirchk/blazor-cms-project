using CMSProject.Core.Interfaces;
using CMSProject.Core.Entities;

namespace CMSProject.Application.Services
{
    public class DashboardService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediaRepository _mediaRepository;

        public DashboardService(
            IContentRepository contentRepository,
            ICategoryRepository categoryRepository,
            IMediaRepository mediaRepository)
        {
            _contentRepository = contentRepository;
            _categoryRepository = categoryRepository;
            _mediaRepository = mediaRepository;
        }

        public async Task<DashboardStats> GetDashboardStatsAsync()
        {
            var allContent = await _contentRepository.GetAllAsync();
            var allCategories = await _categoryRepository.GetAllAsync();
            var allMedia = await _mediaRepository.GetAllAsync();
            
            return new DashboardStats
            {
                TotalContent = allContent.Count(),
                PublishedContent = allContent.Count(c => c.Status == ContentStatus.Published),
                DraftContent = allContent.Count(c => c.Status == ContentStatus.Draft),
                TotalCategories = allCategories.Count(),
                TotalMediaFiles = allMedia.Count(),
                RecentContent = allContent
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(5)
                    .ToList()
            };
        }
    }

    public class DashboardStats
    {
        public int TotalContent { get; set; }
        public int PublishedContent { get; set; }
        public int DraftContent { get; set; }
        public int TotalCategories { get; set; }
        public int TotalMediaFiles { get; set; }
        public List<Content> RecentContent { get; set; } = new();
    }
}
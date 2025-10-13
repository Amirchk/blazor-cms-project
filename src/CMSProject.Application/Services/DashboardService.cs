using CMSProject.Core.Interfaces;

namespace CMSProject.Application.Services
{
    public class DashboardService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DashboardService(
            IContentRepository contentRepository,
            ICategoryRepository categoryRepository)
        {
            _contentRepository = contentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<DashboardStats> GetStatsAsync()
        {
            var allContent = await _contentRepository.GetAllAsync();
            var allCategories = await _categoryRepository.GetAllAsync();

            var contentList = allContent.ToList();

            return new DashboardStats
            {
                TotalContent = contentList.Count,
                PublishedContent = contentList.Count(c => c.Status == Core.Entities.ContentStatus.Published),
                DraftContent = contentList.Count(c => c.Status == Core.Entities.ContentStatus.Draft),
                TotalCategories = allCategories.Count(),
                TotalMediaFiles = 0, // Will implement when we add media feature
                RecentContent = contentList.OrderByDescending(c => c.CreatedDate).Take(5).ToList()
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
        public List<Core.Entities.Content> RecentContent { get; set; } = new();
    }
}
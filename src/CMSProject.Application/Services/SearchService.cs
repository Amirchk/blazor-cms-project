using CMSProject.Core.Interfaces;
using CMSProject.Core.Entities;
using CMSProject.Core;

public class SearchService : ISearchService
{
    private readonly IContentRepository _contentRepository;

    public SearchService(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<SearchResult> SearchAsync(string query, bool publicOnly = true)
    {
        ContentStatus? status = publicOnly ? ContentStatus.Published : (ContentStatus?)null;
        var results = await _contentRepository.SearchAsync(query, status);

        return new SearchResult
        {
            Query = query,
            TotalResults = results.Count(),
            Results = results.Select(c => new SearchResultItem
            {
                Id = c.Id,
                Title = c.Title,
                Excerpt = GetExcerpt(c.Body, query),
                Url = $"/blog/{c.Slug}",
                PublishedDate = c.PublishedDate
            }).ToList()
        };
    }

    private string GetExcerpt(string content, string query, int length = 200)
    {
        if (string.IsNullOrEmpty(content)) return string.Empty;

        var normalizedContent = content.ToLowerInvariant();
        var normalizedQuery = query.ToLowerInvariant();
        var index = normalizedContent.IndexOf(normalizedQuery);

        if (index == -1) return content.Substring(0, Math.Min(length, content.Length));

        var start = Math.Max(0, index - length / 2);
        var end = Math.Min(content.Length, index + length / 2);
        var excerpt = content.Substring(start, end - start);

        return start > 0 ? $"...{excerpt}..." : $"{excerpt}...";
    }
}

public interface IContentRepository
{
    Task<IEnumerable<Content>> SearchAsync(string query, ContentStatus? status);
}
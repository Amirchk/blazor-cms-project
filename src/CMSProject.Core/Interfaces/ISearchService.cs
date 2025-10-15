public interface ISearchService
{
    Task<SearchResult> SearchAsync(string query, bool publicOnly = true);
}
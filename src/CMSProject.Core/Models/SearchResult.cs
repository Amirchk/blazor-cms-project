public class SearchResult
{
    public string Query { get; set; }
    public int TotalResults { get; set; }
    public List<SearchResultItem> Results { get; set; } = new();
}

public class SearchResultItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Excerpt { get; set; }
    public string Url { get; set; }
    public DateTime? PublishedDate { get; set; }
}
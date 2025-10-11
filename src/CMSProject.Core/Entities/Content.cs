namespace CMSProject.Core.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public ContentStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string MetaTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;

        // Navigation properties
        public Category? Category { get; set; }
    }

    public enum ContentStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2
    }
}
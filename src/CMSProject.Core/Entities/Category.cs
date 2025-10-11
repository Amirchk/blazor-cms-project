namespace CMSProject.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public Category? Parent { get; set; }
        public ICollection<Category> Children { get; set; } = new List<Category>();
        public ICollection<Content> Contents { get; set; } = new List<Content>();
    }
}
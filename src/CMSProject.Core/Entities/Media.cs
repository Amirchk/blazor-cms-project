namespace CMSProject.Core.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string MimeType { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public string AltText { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
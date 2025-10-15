using CMSProject.Core.Interfaces;
using System.Text;
using System.Xml;

namespace CMSProject.Web.Services
{
    public class SitemapService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SitemapService(IContentRepository contentRepository, ICategoryRepository categoryRepository)
        {
            _contentRepository = contentRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<string> GenerateSitemapXmlAsync(string baseUrl)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (var writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                // Add homepage
                WriteUrl(writer, $"{baseUrl}/blog", DateTime.Now, "daily", "1.0");

                // Add published articles
                var articles = await _contentRepository.GetPublishedAsync();
                foreach (var article in articles)
                {
                    var lastMod = article.ModifiedDate ?? article.PublishedDate ?? article.CreatedDate;
                    WriteUrl(writer, $"{baseUrl}/blog/{article.Slug}", lastMod, "weekly", "0.8");
                }

                // Add categories
                var categories = await _categoryRepository.GetAllAsync();
                foreach (var category in categories.Where(c => c.ParentId == null))
                {
                    WriteUrl(writer, $"{baseUrl}/blog/category/{category.Slug}", DateTime.Now, "weekly", "0.6");
                }

                // Add categories page
                WriteUrl(writer, $"{baseUrl}/blog/categories", DateTime.Now, "weekly", "0.7");

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return sb.ToString();
        }

        private void WriteUrl(XmlWriter writer, string loc, DateTime lastMod, string changeFreq, string priority)
        {
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", loc);
            writer.WriteElementString("lastmod", lastMod.ToString("yyyy-MM-dd"));
            writer.WriteElementString("changefreq", changeFreq);
            writer.WriteElementString("priority", priority);
            writer.WriteEndElement();
        }
    }
}
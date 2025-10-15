using CMSProject.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMSProject.Web.Controllers
{
    [Route("[controller]")]
    public class SitemapController : Controller
    {
        private readonly SitemapService _sitemapService;
        private readonly IConfiguration _configuration;

        public SitemapController(SitemapService sitemapService, IConfiguration configuration)
        {
            _sitemapService = sitemapService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("sitemap.xml")]
        public async Task<IActionResult> Index()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var xml = await _sitemapService.GenerateSitemapXmlAsync(baseUrl);
            return Content(xml, "application/xml");
        }
    }
}
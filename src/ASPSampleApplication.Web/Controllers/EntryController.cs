using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPSampleApplication.Web.Controllers
{
    [Route("api/entry")]
    public class EntryController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IArticleSearchService _articleSearchService;

        public EntryController(IArticleService articleService, IArticleSearchService articleSearchService)
        {
            _articleService = articleService;
            _articleSearchService = articleSearchService;
        }

        [HttpGet("{id}")]
        public async Task<Article> Index(string id)
        {
            var result = await _articleService.GetAsync(id);

            return result;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            await _articleService.SaveAsync(new[] { article });

            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Article article)
        {
            await _articleService.SaveAsync(new[] { article });

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _articleService.DeleteAsync(new[] { id });

            return Ok();
        }

        [HttpPost("search")]
        public async Task<ArticleSearchResult> Search([FromBody] ArticleSearchCriteria searchCriteria)
        {
            var result = await _articleSearchService.SearchAsync(searchCriteria);

            return result;
        }
    }
}

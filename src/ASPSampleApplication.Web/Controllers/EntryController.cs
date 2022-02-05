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

        /// <summary>
        /// Returns the requested article by Id.
        /// Allows anonymous access
        /// </summary>
        /// <param name="id">An article id</param>
        /// <returns><see cref="Article"/>Article object</returns>
        [HttpGet("{id}")]
        public async Task<Article> Index(string id)
        {
            var result = await _articleService.GetAsync(id);

            return result;
        }

        /// <summary>
        /// Add particular article.
        /// Authorization is required.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            await _articleService.SaveAsync(new[] { article });

            return Ok();
        }

        /// <summary>
        /// Updating the article.
        /// Authorization is required.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Article article)
        {
            await _articleService.SaveAsync(new[] { article });

            return Ok();
        }

        /// <summary>
        /// Deleting the article by id.
        /// Authorization is required.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _articleService.DeleteAsync(new[] { id });

            return Ok();
        }

        /// <summary>
        /// Represents the paging search ability.
        /// Allows anonymous requests.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<ArticleSearchResult> Search([FromBody] ArticleSearchCriteria searchCriteria)
        {
            var result = await _articleSearchService.SearchAsync(searchCriteria);

            return result;
        }
    }
}

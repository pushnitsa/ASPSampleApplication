using ASPSampleApplication.Core.Models;
using ASPSampleApplication.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPSampleApplication.Web.Controllers
{
    [Route("api/entry")]
    public class EntryController : Controller
    {
        private readonly IArticleService _articleService;

        public EntryController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}")]
        public async Task<Article> Index(string id)
        {
            var result = await _articleService.GetAsync(id);

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            await _articleService.SaveAsync(new[] { article });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Article article)
        {
            await _articleService.SaveAsync(new[] { article });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _articleService.DeleteAsync(new[] { id });

            return Ok();
        }

        [HttpPost("search")]
        public IActionResult Search()
        {
            return View();
        }
    }
}

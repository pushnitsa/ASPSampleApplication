using Microsoft.AspNetCore.Mvc;

namespace ASPSampleApplication.Web.Controllers
{
    [Route("api/entry")]
    public class EntryController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPut]
        public IActionResult Update()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search()
        {
            return View();
        }
    }
}

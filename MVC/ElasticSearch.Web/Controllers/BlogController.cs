using ElasticSearch.Web.Services;
using ElasticSearch.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(CreateBlogViewModel createBlogViewModel)
        {
            var isSuccess = await _blogService.SaveAsync(createBlogViewModel);
            if (!isSuccess)
                TempData["result"] = "Kayıt Başarısız";
            else
                TempData["result"] = "Kayıt Başarılı";
            return RedirectToAction(nameof(Save));
        }

        public async Task<IActionResult> Search()
        {
            return View(await _blogService.SearchAsync(string.Empty));
        }
        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {

            return View(await _blogService.SearchAsync(searchText));
        }
    }
}

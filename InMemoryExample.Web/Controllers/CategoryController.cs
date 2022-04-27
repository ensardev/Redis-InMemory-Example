using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryExample.Web.Controllers
{
    public class CategoryController : Controller
    {
        private IMemoryCache _memoryCache;
        
        public CategoryController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

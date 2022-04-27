using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

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
            _memoryCache.Set<string>("date", DateTime.Now.ToString());

            return View();
        }

        public IActionResult GetData()
        {
            ViewBag.Date =  _memoryCache.Get<string>("date");
            return View();
        }
    }
}

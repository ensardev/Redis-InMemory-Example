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
            ////Method 1
            //if (String.IsNullOrEmpty(_memoryCache.Get<string>("date")))
            //{
            //    _memoryCache.Set<string>("date", DateTime.Now.ToString());

            //}

            
            //Method 2
            //Zaman ataması yapmak için; 
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            //AbsoluteExpration example
            //options.AbsoluteExpiration = DateTime.Now.AddSeconds(20);

            //SlidingExpiration example
            //options.SlidingExpiration = TimeSpan.FromSeconds(10);

            //Cache item expire in absolute time with sliding expiration
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            options.SlidingExpiration = TimeSpan.FromSeconds(10);


            _memoryCache.Set<string>("date", DateTime.Now.ToString(), options);

            return View();
        }

        public IActionResult GetData()
        {
            //_memoryCache.GetOrCreate<string>("date", entry => DateTime.Now.ToString());

            _memoryCache.TryGetValue("date", out string dateCache);

            ViewBag.Date = dateCache;
            return View();
        }
    }
}

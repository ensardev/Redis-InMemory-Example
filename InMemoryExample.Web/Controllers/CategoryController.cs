using InMemoryExample.Web.Models;
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
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(15);
            //options.SlidingExpiration = TimeSpan.FromSeconds(10);

            //We set priorty to make sure that the cache item will be removed first
            options.Priority = CacheItemPriority.High;
            //First deleted low priority -> Level going Low - Normal - High - NeverRemove (Never never :D )


            //RegisterPostEvictionCallback example
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                _memoryCache.Set("callback", $"Key: {key} value: {value} reason: {reason} substate: {state}");
            });

            _memoryCache.Set<string>("date", DateTime.Now.ToString(), options);


            //Complex types caching example
            Category category = new Category { Id = 1, Name = "Category 1", Description = "Category 1 description" };

            _memoryCache.Set<Category>("category:1", category, options);


            return View();
        }

        public IActionResult GetData()
        {
            //_memoryCache.GetOrCreate<string>("date", entry => DateTime.Now.ToString());

            _memoryCache.TryGetValue("date", out string dateCache);
            _memoryCache.TryGetValue("callback", out string callbackCache);

            ViewBag.Date = dateCache;
            ViewBag.Callback = callbackCache;

            ViewBag.Category = _memoryCache.Get<Category>("category:1");


            return View();
        }
    }
}

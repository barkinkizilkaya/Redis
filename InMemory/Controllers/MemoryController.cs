using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Controllers
{
    public class MemoryController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
    

            var value = _memoryCache.GetOrCreate("Time", entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddSeconds(100);
                entry.SlidingExpiration = TimeSpan.FromSeconds(20);
                entry.Priority = CacheItemPriority.NeverRemove;
                entry.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    _memoryCache.Set("CallBack", $"{key}--> {value} : reasion:{reason}");
                });
                return DateTime.Now.ToLongTimeString();
            });
            return View();
     
        }

        public IActionResult Show()
        {
            ViewBag.Time = _memoryCache.Get<string>("Time");
            return View();
        }

    }
}
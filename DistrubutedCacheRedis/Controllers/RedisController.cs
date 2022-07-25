using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistrubutedCacheRedis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DistrubutedCacheRedis.Controllers
{
    public class RedisController : Controller
    {

        private readonly IDistributedCache _redisCache;

        public RedisController(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public IActionResult Index()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            _redisCache.SetString("RedisCache", "Hello", cacheOptions);
            return View();
        }


        public IActionResult Show()
        {
            ViewBag.Message =  _redisCache.GetString("RedisCache");
            //to remove data from cache
           _redisCache.Remove("RedisCache");
            return View();
        }

        public async Task<IActionResult> Complex()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(20);
            Car car = new Car { Id = 1, Age = 2, Model = "Audi" };
            string jsonCar = JsonConvert.SerializeObject(car);
            await  _redisCache.SetStringAsync("car:1", jsonCar, cacheOptions);
            return View();
        }

        public IActionResult CDataShow()
        {
            string jsonCar = _redisCache.GetString("car:1");
            Car c = JsonConvert.DeserializeObject<Car>(jsonCar);
            ViewBag.Name = c.Model;
            return View();
        }

        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/image.png");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            _redisCache.Set("image", imageByte);
            return View();
        }

        public IActionResult ShowImage()
        {
            byte[] imageByte = _redisCache.Get("image");
            return File(imageByte, "image/png");
        }
    }
}
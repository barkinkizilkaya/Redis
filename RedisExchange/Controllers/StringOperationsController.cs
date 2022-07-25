using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchange.Services;
using StackExchange.Redis;

namespace InMemory.Controllers
{
    public class StringOperationsController : Controller
    {
        private readonly RedisService _reddisService;

        private readonly IDatabase _db;

        public StringOperationsController(RedisService reddisService)
        {
            _reddisService = reddisService;
            _db = _reddisService.GetDb(1);
        }

        public IActionResult Index()
        {
           
            _db.StringSet("Data", "Barkın Kızılkaya");
            _db.StringSet("Count", 500);
            return View();

        }

        public async Task<IActionResult> Show()
        {
           var value =  _db.StringGet("Data");
            _db.StringIncrement("Count", 1);
            await _db.StringDecrementAsync("Count", 5);
            _db.StringLength("Data");
            if(value.HasValue)
            {
                ViewBag.value = value;
            }
            return View();
        }
    }
}
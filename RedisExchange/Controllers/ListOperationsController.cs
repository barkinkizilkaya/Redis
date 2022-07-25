using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchange.Services;
using StackExchange.Redis;

namespace InMemory.Controllers
{
    public class ListOperationsController : Controller
    {
        private readonly RedisService _reddisService;

        private readonly IDatabase _db;

        public ListOperationsController(RedisService reddisService)
        {
            _reddisService = reddisService;
            _db = _reddisService.GetDb(2);
        }

        public IActionResult Index()
        {

            _db.ListRightPush("List", 1);

            return View();
        }
    }
}
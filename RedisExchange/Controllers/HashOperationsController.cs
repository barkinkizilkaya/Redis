using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InMemory.Controllers
{
    public class HashOperationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
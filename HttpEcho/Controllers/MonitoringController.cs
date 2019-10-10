using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Controllers
{
    public class MonitoringController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return Content("Monitoring Index!");
        }
    }
}

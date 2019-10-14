using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Controllers
{
    public class RequestController : Controller
    {
        public async Task<IActionResult> Incoming(string url, string userId, string endpoint)
        {
            return Content($"{userId}/{endpoint}/{url}");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(string userId)
        {
            return Content("User Index!");
        }
    }
}

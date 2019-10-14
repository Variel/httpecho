using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Areas.Intro.Controllers
{
    [Area("Intro")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}

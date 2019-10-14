using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Areas.User.Controllers
{
    public class ConfigController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return Content("Config Index!");
        }
    }
}

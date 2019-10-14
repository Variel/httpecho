using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpEcho.Areas.User.Controllers
{
    public class MonitoringController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return Content("Monitoring Index!");
        }
    }
}

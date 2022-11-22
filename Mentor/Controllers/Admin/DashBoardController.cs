using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Jobs()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
    }
}

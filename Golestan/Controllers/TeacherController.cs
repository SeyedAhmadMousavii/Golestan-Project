using Microsoft.AspNetCore.Mvc;

namespace Golestan.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.Message = "به داشبورد استاد خوش آمدید.";
            return View();
        }
    }
}

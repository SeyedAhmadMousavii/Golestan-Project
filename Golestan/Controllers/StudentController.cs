using Microsoft.AspNetCore.Mvc;

namespace Golestan.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.Message = "به داشبورد دانشجو خوش آمدید.";

            return View();
        }
    }
}

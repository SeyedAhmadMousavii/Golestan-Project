using Microsoft.AspNetCore.Mvc;

namespace Golestan.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Golestan.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var User = _context.Users.FirstOrDefault(U => U.Id == model.Id && U.Hashed_password == model.Password);

            if (User == null)
            {
                ModelState.AddModelError("", "کد ملی یا رمز عبور اشتباه است.");
                return View(model);
            }

            var role = User.User_Roles.Any(UR => UR.roles.Name.ToString() == model.SelectedRole);

            if (!role)
            {
                ModelState.AddModelError("", "نقش انتخاب شده با نقش های شما مطابقط نداره");
                return View(model);
            }
            if (model.SelectedRole == "ادمین")
            {
                return RedirectToAction("Login", "Account");//this line most be change to addmin page
            }
            else if (model.SelectedRole == "استاد")
            {
                return RedirectToAction("Login", "Account");//...........................teacher page
            }
            else if (model.SelectedRole == "دانشجو")
            {
                return RedirectToAction("Login", "Account");//............................student page
            }
            else { return View(model);}

        }
    }
}

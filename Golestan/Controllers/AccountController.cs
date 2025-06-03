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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id && u.Hashed_password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "کد ملی یا رمز عبور اشتباه است.");
                return View(model);
            }

            var hasRole = _context.User_Roles.Any(ur => ur.User_Id == user.Id && ur.roles.Name.ToString() == model.SelectedRole);
            if (!hasRole)
            {
                ModelState.AddModelError("", "نقش انتخاب‌شده با نقش‌های شما مطابقت ندارد.");
                return View(model);
            }
            if (model.SelectedRole == "Admin")//should be changef
            {
                return View(model);
            }
            if (model.SelectedRole == "Teacher")//should be changef
            {
                return View(model);
            }
            if (model.SelectedRole == "Student")//should be changef
            {
                return View(model);
            }
            return View(model);
        }

    }
}

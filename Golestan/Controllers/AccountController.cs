using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id && u.Hashed_password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "کد یا رمز اشتباه است.");
                return View(model);
            }


            if (!Enum.TryParse<role>(model.SelectedRole, out var parsedRole))
            {
                ModelState.AddModelError("", "نقش انتخابی معتبر نیست.");
                return View(model);
            }

            var hasRole = await _context.User_Roles
                .Include(ur => ur.roles)
                .AnyAsync(ur => ur.User_Id == user.Id && ur.roles.Name == parsedRole);

            if (!hasRole)
            {
                ModelState.AddModelError("", "نقش انتخاب‌شده با نقش‌های شما مطابقت ندارد.");
                return View(model);
            }

            switch (model.SelectedRole)
            {
                case "Admin":
                    return RedirectToAction("Index", "Admin");
                case "Teacher":
                    return RedirectToAction("Dashboard", "Teacher", new { id = user.Id }); 
                case "Student":
                    return RedirectToAction("Dashboard", "Student" , new {id =  user.Id}); 
                default:
                    ModelState.AddModelError("", "نقش نامعتبر است.");
                    return View(model);
            }
        }
    }
}

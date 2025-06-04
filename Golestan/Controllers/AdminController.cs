using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Golestan.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(); 
        }

        // افزودن درس
        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(string title)
        {
            var course = new Courses { Title = title };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // افزودن کلاس برای درس
        [HttpGet]
        public IActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddClass(string name, int courseId)
        {
            var classItem = new Classrooms { Building = name, Id = courseId };
            _context.Classrooms.Add(classItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // افزودن استاد
        [HttpGet]
        public IActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher(string fullName, string username, string password)
        {
            var user = new Users
            {
                First_name = fullName,
                Email = username,
                Hashed_password = password,
                Created_at = DateTime.Now
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.User_Roles.Add(new User_Role { User_Id = user.Id, Role_Id = 2 });
            await _context.SaveChangesAsync();

            var teacher = new Instructors {  User_Id = user.Id };
            _context.Instructors.Add(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // افزودن دانشجو
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(string fullName, string username, string password)
        {
            var user = new Users
            {
                First_name = fullName,
                Email = username,
                Hashed_password = password,
                Created_at = DateTime.Now
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.User_Roles.Add(new User_Role { User_Id = user.Id, Role_Id = 1 }); // RoleId = 1 = Student
            await _context.SaveChangesAsync();

            var student = new Students {  User_Id = user.Id };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Admin");
        }

        // تخصیص استاد به کلاس
        [HttpGet]
        public IActionResult AssignTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignTeacher(int classId, int teacherId)
        {
            var classItem = await _context.Students.FindAsync(classId);
            if (classItem != null)
            {
                classItem.Id = teacherId;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // افزودن دانشجو به کلاس
        [HttpGet]
        public IActionResult AddStudentToClass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudentToClass(int studentId, int classId)
        {
            bool exists = await _context.Classrooms.AnyAsync();
            if (!exists)
            {
                _context.Classrooms.Add(new Classrooms { Id=classId });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // لغو تخصیص استاد از کلاس
        [HttpGet]
        public IActionResult RemoveTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveTeacher(int classId)
        {
            var classItem = await _context.Classrooms.FindAsync(classId);
            if (classItem != null)
            {
                classItem.Building = null;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // لغو تخصیص دانشجو از کلاس
        [HttpGet]
        public IActionResult RemoveStudentFromClass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveStudentFromClass(int studentId, int classId)
        {
            var record = await _context.Classrooms.FirstOrDefaultAsync();
            if (record != null)
            {
                _context.Classrooms.Remove(record);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // حذف درس
        [HttpGet]
        public IActionResult DeleteCourse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // حذف کلاس
        [HttpGet]
        public IActionResult DeleteClass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classItem = await _context.Classrooms.FindAsync(id);
            if (classItem != null)
            {
                _context.Classrooms.Remove(classItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // حذف استاد
        [HttpGet]
        public IActionResult DeleteTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Instructors.FindAsync(id);
            if (teacher != null)
            {
                _context.Instructors.Remove(teacher);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // حذف دانشجو
        [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

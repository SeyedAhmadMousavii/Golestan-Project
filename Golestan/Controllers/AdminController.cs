using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
<<<<<<< Updated upstream
            return View(); // مطمئن شو که View مربوطه ساخته شده (Views/Admin/Index.cshtml)
        }

        // افزودن درس
        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }
=======

            return View();
        }

   
        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> AddCourse(string title)
        {
            var course = new Course { Title = title };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // افزودن کلاس برای درس
=======

>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult AddClass()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> AddClass(string name, int courseId)
        {
            var classItem = new Class { Name = name, CourseId = courseId };
            _context.Classes.Add(classItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // افزودن استاد
=======
>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult AddTeacher()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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

            var teacher = new Teacher { FullName = fullName, Username = username, Password = password, User_Id = user.Id };
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // افزودن دانشجو
=======

>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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

            var student = new Student { FullName = fullName, Username = username, Password = password, User_Id = user.Id };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Admin");
        }


        [HttpGet]
        public IActionResult AssignTeacher()
        {
            return View();
        }

<<<<<<< Updated upstream
        // تخصیص استاد به کلاس
        [HttpGet]
        public IActionResult AssignTeacher()
        {
            return View();
        }
=======
>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> AssignTeacher(int classId, int teacherId)
        {
            var classItem = await _context.Classes.FindAsync(classId);
            if (classItem != null)
            {
                classItem.TeacherId = teacherId;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // افزودن دانشجو به کلاس
=======

>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult AddStudentToClass()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> AddStudentToClass(int studentId, int classId)
        {
            bool exists = await _context.Student_Classes.AnyAsync(sc => sc.StudentId == studentId && sc.ClassId == classId);
            if (!exists)
            {
                _context.Student_Classes.Add(new Student_Class { StudentId = studentId, ClassId = classId });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // لغو تخصیص استاد از کلاس
=======
>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult RemoveTeacher()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> RemoveTeacher(int classId)
        {
            var classItem = await _context.Classes.FindAsync(classId);
            if (classItem != null)
            {
                classItem.TeacherId = null;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // لغو تخصیص دانشجو از کلاس
=======

>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult RemoveStudentFromClass()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> RemoveStudentFromClass(int studentId, int classId)
        {
            var record = await _context.Student_Classes.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.ClassId == classId);
            if (record != null)
            {
                _context.Student_Classes.Remove(record);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // حذف درس
=======
>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult DeleteCourse()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        // حذف کلاس
=======

>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult DeleteClass()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classItem = await _context.Classes.FindAsync(id);
            if (classItem != null)
            {
                _context.Classes.Remove(classItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

<<<<<<< Updated upstream
        // حذف استاد
=======
>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult DeleteTeacher()
        {
            return View();
        }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [HttpPost]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }

<<<<<<< Updated upstream
        // حذف دانشجو
        [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }
=======
>>>>>>> Stashed changes
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

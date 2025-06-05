using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        //افزودن کاربر
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(string First_Name,string Last_Name,string Email,string Password)
        {
            var user = new Users { First_name=First_Name,Last_name=Last_Name,Email=Email,Hashed_password=Password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        // افزودن درس
        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

            return View();
        }

   
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
            var user = _context.Users.ToList();
            var departments = _context.Departments
                                      .Select(d => new SelectListItem
                                      {
                                          Value = d.Id.ToString(),
                                          Text = d.Name
                                      }).ToList();

            ViewBag.Departments = departments;

            return View(user);
        }
  
        [HttpPost]
        public async Task<IActionResult> AddStudent(int Id,int StudID,int DepartID)
        {
    
            var user = await _context.Users.FindAsync(Id);
            bool alreadyExists = _context.User_Roles.Any(ur => ur.User_Id == Id && ur.Role_Id == 2);
            if (user == null)
            {
                //adding error message
                return RedirectToAction("Index");
            }
            else
            {
                if (user.User_Roles!=null || !alreadyExists )
                {
                    var newUserRole = new User_Role
                    {
                        User_Id = Id,
                        Role_Id = 2
                    };
                    _context.User_Roles.Add(newUserRole);
                    _context.SaveChanges();
                }

                var student = new Students
                {
                    User_Id = Id,
                    Student_Id = StudID,
                    Depatment_Id = DepartID,
                    Enrollment_Date = DateTime.Now,
                    User=user
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index","Admin");
        }


        [HttpGet]
        public IActionResult AssignTeacher()
        {
            return View();
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
        // حذف کاربر
        [HttpGet]
        public IActionResult DeleteUser()
        {
            var user = _context.Users.ToList();
            return View(user);
        }
        [HttpPost]
        public async  Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User != null)
            {
                var students = _context.Students.Where(s => s.User_Id == id).ToList();

                if (students.Any())
                {
                    _context.Students.RemoveRange(students);
                }
                var instructor = _context.Instructors.Where(s => s.User_Id == id).ToList();

                if (instructor.Any())
                {
                    _context.Instructors.RemoveRange(instructor);
                }
              
                _context.Users.Remove(User);
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
        [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }
  
        // حذف دانشجو
        [HttpGet]
        public IActionResult DeleteStudent()
        {
            var user = _context.Students.ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var Student = await _context.Students.FindAsync(Id);
            if (Student != null)
            {
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

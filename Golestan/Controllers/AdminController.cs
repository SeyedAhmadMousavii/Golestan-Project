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
        public async Task<IActionResult> AddUser(int UserId,string First_Name,string Last_Name,string Email,string Password)
        {
            var user = new Users 
            { 
                UserId=UserId,
                First_name=First_Name,
                Last_name=Last_Name,
                Email=Email,
                Hashed_password=Password 
            };
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
        [HttpPost]
        public async Task<IActionResult> AddCourse(int id,string title,string code,string unit,string description,DateTime final,int DepartID)
        {
            var course = new Courses 
            { 
                  Title = title
                , CoursId = id
                , Code = code
                , Unit = unit
                ,Description = description
                ,Final_Exam_Date = final
                ,Department_Id=DepartID
            };
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
        public async Task<IActionResult> AddClass(int SID,int year,int semester, int courseId,int TimeId,int classs)
        {
            var cours =await _context.Courses.FirstOrDefaultAsync(c=>c.CoursId==courseId);

            var classItem = new Sections
            {
                SectionId= SID,
                Course_Id = cours.Id,
                courses=cours,
                Semester =semester,
                year = year,
                Time_Slot_Id = TimeId,
                Classroom_Id= classs

            };
            _context.Sections.Add(classItem);
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
        public async Task<IActionResult> AddTeacher(int Id,int TeachId, decimal salary, int DepartId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId==Id);
            bool alreadyExists = _context.User_Roles.Any(ur => ur.User_Id == user.Id && ur.Role_Id == 1);
            if (user == null)
            {
                //adding error message
                return RedirectToAction("Index");
            }
            else
            {
                if (user.User_Roles != null || !alreadyExists)
                {
                    var newUserRole = new User_Role
                    {
                        User_Id = user.Id,
                        Role_Id = 2
                    };
                    _context.User_Roles.Add(newUserRole);
                    _context.SaveChanges();
                }

                var instructor = new Instructors
                {
                    User_Id = user.Id,
                    Instructor_Id = TeachId,
                    Department_Id = DepartId,
                    Salary = salary,
                    Hire_Date = DateTime.Now,
                    User = user
                };
                _context.Instructors.Add(instructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Admin");
        }

        // افزودن دانشجو

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
  
        [HttpPost]
        public async Task<IActionResult> AddStudent(int Id,int StudID,int DepartID)
        {
    
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId==Id);
            bool alreadyExists = _context.User_Roles.Any(ur => ur.User_Id == user.Id && ur.Role_Id == 2);
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
                        User_Id = user.Id,
                        Role_Id = 1
                    };
                    _context.User_Roles.Add(newUserRole);
                    _context.SaveChanges();
                }

                var student = new Students
                {
                    User_Id = user.Id,
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

        // تخصیص استاد به کلاس
        [HttpGet]
        public IActionResult AssignTeacher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignTeacher(int classId, int teacherId)
        {
            var classItem = await _context.Sections.FirstOrDefaultAsync(c=>c.SectionId==classId);

            var instructor = await _context.Instructors.FirstOrDefaultAsync(i=>i.Instructor_Id==teacherId);

            bool hasvalid = _context.Teaches.Any(t => t.Instructor_Id == classItem.Id && t.Section_Id == instructor.Id);

            if(classItem == null || instructor == null)
            {
                return NotFound();
            }
            if (!hasvalid)
            {
                var teach = new Teaches 
                {
                    Section_Id = classItem.Id,
                    Instructor_Id = instructor.Id
                };
                _context.Teaches.Add(teach);
                _context.Instructors.Find(instructor.Id).teaches.Add(teach);
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
            var section = await _context.Sections.FirstOrDefaultAsync(s=>s.SectionId==classId);
            var student = await _context.Students.FirstOrDefaultAsync(S=>S.Student_Id==studentId);

            Console.WriteLine($"----- suid{section.Id} cuid{student.Id}------------");

            if (section!=null && student!=null)
            {
                var takess = new Takes
                {
                    Section_Id = section.Id,
                    Student_Id = student.Id,
                    Grade = "0"
                };
                //student.takes.Add(takess);
                //section.takes.Add(takess);
                _context.Takes.Add(takess);
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
            var classItem = await _context.Sections.FirstOrDefaultAsync(s=>s.SectionId==classId);
            var teach = await _context.Teaches.FirstOrDefaultAsync(t => t.Section_Id == classItem.Id);
            
            if (classItem != null)
            {
                classItem.teaches = null;

                var ins = _context.Instructors.Where(i => i.teaches.Contains(teach));
                foreach(var i in ins)
                {
                    i.teaches.Remove(teach);
                }

                
                _context.Teaches.Remove(teach);
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
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Student_Id == studentId);
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.SectionId == classId);

            var record = await _context.Takes.FirstOrDefaultAsync(t=> t.Section_Id==section.Id && t.Student_Id==student.Id);
            if (record != null)
            {
                _context.Takes.Remove(record);
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
            var course = await _context.Courses.FirstOrDefaultAsync(x=>x.CoursId==id);
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
            var User = await _context.Users.FirstOrDefaultAsync(u=>u.UserId==id);
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
            var classItem = await _context.Sections.FirstOrDefaultAsync(c=>c.SectionId==id);
            if (classItem != null)
            {
                _context.Sections.Remove(classItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
  
        // حذف استاد

        [HttpGet]
        public IActionResult DeleteTeacher()
        {
            var user = _context.Instructors.ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Instructors.FirstOrDefaultAsync(i=>i.Instructor_Id==id);
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
            var user = _context.Students.ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var Student = await _context.Students.FirstOrDefaultAsync(s=>s.Student_Id==Id);
            if (Student != null)
            {
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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
            var isvalid = _context.Users.Any(u=>u.UserId==UserId);

            if (isvalid)
            {
                ViewBag.ErrorMessage="شناسه کاربر تکراریه";
                return View();
            }

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
        public async Task<IActionResult> AddCourse(int id,string Title,string code,string unit,string description,DateTime final,int DepartID)
        {
            var isvalid = _context.Courses.Any(c=>c.CoursId == id);
            if (isvalid)
            {
                ViewBag.ErrorMessage = "شناسه درس تکراریه";
                return View();
            }
         
            var course = new Courses 
            { 
                  Title = Title
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
            if (cours == null) { ViewBag.ErrorMessage = "کلاس پیدا نشد";return View(); }

            var isvalid = _context.Sections.Any(i => i.SectionId == SID);

            var isvalidlocation = _context.Sections.Any(s=>s.Time_Slot_Id==TimeId && s.Classroom_Id==classs);

            var isvalidcourse = _context.Sections.Any(s=>s.Course_Id==courseId);
            if (isvalid)
            {
                ViewBag.ErrorMessage = "شناسه کلاس تکراری است";
                return View();
            }
            if(year<DateTime.Now.Year ||semester<1||semester>3)
            {
                ViewBag.ErrorMessage = "سال یه ترم کلاس اشتباه است";
                return View();
            }
            if (isvalidlocation)
            {
                ViewBag.ErrorMessage = "لوکیشن کلاس اشغال است";
                return View();
            }
            if (isvalidcourse)
            {
                ViewBag.ErrorMessage = "درس انتخاب شده مال کلاس دیگری است";
                return View();
            }

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
            if (user == null)
            {
                ViewBag.ErrorMessage = "کاربر پیدا نشد";
                return View();
            }

            bool alreadyExists = _context.User_Roles.Any(ur => ur.User_Id == user.Id && ur.Role_Id == 2);
        
            var isvalid = _context.Instructors.Any(i=>i.Instructor_Id==TeachId);
            if (isvalid)
            {
                ViewBag.ErrorMessage = "شناسه استاد تکراری است";
                return View();
            }
            if (salary < 1)
            {
                ViewBag.ErrorMessage = "مقدار حقوق نامعتبر است";
                return View();
            }

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
            var uuuu = _context.Users.Any(u=>u.UserId==Id);

            if (user == null || ! uuuu)
            {
                ViewBag.ErrorMessage = "کاربر پیدا نشد";
                return View();
            }

            bool alreadyExists = _context.User_Roles.Any(ur => ur.User_Id == user.Id && ur.Role_Id == 1);

            var isvalid = _context.Students.Any(i => i.Student_Id ==StudID);
            if (isvalid)
            {
                ViewBag.ErrorMessage = "شناسه دانشجو تکراری است";
                return View();
            }

            if(user.User_Roles!=null || !alreadyExists )
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

            if (classItem == null)
            {
                ViewBag.ErrorMessage = "کلاس پیدا نشد";
                return View();
            }
            if (instructor == null)
            {
                ViewBag.ErrorMessage = "استاد پیدا نشد";
                return View();
            }
            var cours = _context.Courses.Find(classItem.Course_Id);
            if (cours == null)
            {
                ViewBag.ErrorMessage = "درس پیدا نشد";
                return View();
            }

            bool hasvalid = _context.Teaches.Any(t => t.Section_Id == classItem.SectionId);

            if (hasvalid)
            {
                ViewBag.ErrorMessage = "کلاس مال استاد دیگری است";
                return View();
            }
            if (cours.Department_Id != instructor.Department_Id)
            {
                ViewBag.ErrorMessage = "استاد مال دانشکده دیگری است";
                return View();
            }
            try
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
            catch (Exception ex) { ViewBag.ErrorMessage = ex.Message; }
            
            
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

            if (section==null)
            {
                ViewBag.ErrorMessage = "کلاس پیدا نشد";
                return View();
            }
            if (student==null)
            {
                ViewBag.ErrorMessage = "دانشجو پیدا نشد";
                return View();
            }

            var departvalid = _context.Courses.Any(c => c.Department_Id == student.Depatment_Id);
            var takevalid = _context.Takes.Any(c=>c.Section_Id==section.Id&&c.Student_Id==student.Id);
            var isvalidstuden = _context.Students.Any(s=>s.Id==student.Id);
            var isvalidsection = _context.Sections.Any(s=>s.Id==section.Id);

            var cours = _context.Courses.Find(section.Course_Id);
            if (cours == null)
            {
                ViewBag.ErrorMessage = "درس پیدا نشد";
                return View();
            }
            if (!isvalidsection )
            {
                ViewBag.ErrorMessage = "کلاس پیدا نشد";
                return View();
            }
            if (!isvalidstuden)
            {
                ViewBag.ErrorMessage = "دانشجو پیدا نشد";
                return View();
            }
            if (!departvalid) 
            {
                ViewBag.ErrorMessage = "دانشکده دانشجو با درس متفاوت است";
                return View();
            }
            if (takevalid) 
            {
                ViewBag.ErrorMessage = "این دانشجو قبلا اضاف شده";
                return View();
            }
            if (cours.Department_Id != student.Depatment_Id)
            {
                ViewBag.ErrorMessage = "دانشجو مال دانشکده دیگری است";
                return View();
            }


            try
            {
                var takess = new Takes
                {
                    Section_Id = section.Id,
                    Student_Id = student.Id,
                    Grade = "0"
                };
                _context.Takes.Add(takess);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { ViewBag.ErrorMessage = ex.Message; }
            
            
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

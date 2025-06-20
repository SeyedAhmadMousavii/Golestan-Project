﻿using Golestan.Data;
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
            var courses = _context.Courses.ToList();
            ViewBag.Courses = courses;
            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(int id,string Title,string code,int unit,string description,DateTime final,int DepartID,int SelectedCourseId)
        {
            var courses = _context.Courses.ToList();
            ViewBag.Courses = courses;
            var isvalid = _context.Courses.Any(c=>c.CoursId == id);
            if (isvalid)
            {
                ViewBag.ErrorMessage = "شناسه درس تکراریه";
                return View(courses);
            }
            if (unit > 4 || unit < 1)
            {
                ViewBag.ErrorMessage = "واحد نا معتبر است";
                return View(courses);
            }

            var course = new Courses 
            { 
                  Title = Title
                , Prerequisite= SelectedCourseId
                , CoursId = id
                , Code = code
                , Unit = unit.ToString()
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
        public async Task<IActionResult> AddClass(int SID,int year,int semester, int courseId,int TimeId,int classs, int capacity)
        {
            var cours =await _context.Courses.FirstOrDefaultAsync(c=>c.CoursId==courseId);
            if (cours == null) { ViewBag.ErrorMessage = "درس پیدا نشد";return View(); }

            var isvalid = _context.Sections.Any(i => i.SectionId == SID);

            var isvalidlocation = _context.Sections.Any(s=>s.Time_Slot_Id==TimeId && s.Classroom_Id==classs);


            var isvalidcourse = _context.Sections.Any(s=>s.Course_Id==cours.Id);
            if (isvalid)
            {
                ViewBag.ErrorMessage = "شناسه کلاس تکراری است";
                return View();
            }
            if(year<DateTime.Now.Year -622 ||semester<1||semester>3)
            {
                ViewBag.ErrorMessage = "سال یا ترم کلاس اشتباه است";
                return View();
            }
            if (isvalidlocation)
            {

                var sectionss = _context.Sections.FirstOrDefault(s => s.Time_Slot_Id == TimeId && s.Classroom_Id == classs);
                if (sectionss!=null)
                {
                    var courses = _context.Courses.Find(sectionss.Course_Id);
                    if (courses != null)
                    {
                        if (courses.Department_Id == cours.Department_Id)
                        {
                            ViewBag.ErrorMessage = "مکان کلاس اشغال است";
                            return View();
                        }
                    }
                }

                
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
                coursetitle=cours.Title,
                Semester =semester,
                year = year,
                Time_Slot_Id = TimeId,
                Classroom_Id= classs,
                Capacity = capacity
                

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
        public async Task<IActionResult> AddTeacher(int Id,int TeachId, decimal salary, int DepartId, DateTime date)
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
            var department = _context.Departments.Find(DepartId);
            if (department.Budget<salary)
            {
                ViewBag.ErrorMessage = "بودجه کافی نیست";
                return View();
            }
            var isrepeatedinstructor = _context.Instructors.Any(s => s.User_Id == user.Id && s.Department_Id == DepartId);
            if (isrepeatedinstructor)
            {
                ViewBag.ErrorMessage = "این استاد قبلا در این دانشکده ثبت شده است";
                return View();
            }




            var instructor = new Instructors
            { 
                User_Id = user.Id,
                Instructor_Id = TeachId,
                Department_Id = DepartId,
                Salary = salary,
                Hire_Date = date,
                User = user,
                instructorname = user.First_name +" "+ user.Last_name
            };
            department.Budget -= salary;
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
            var isrepeatedstudent = _context.Students.Any(s => s.User_Id == user.Id && s.Depatment_Id == DepartID);
            if (isrepeatedstudent)
            {
                ViewBag.ErrorMessage = "این دانشجو قبلا در این دانشکده ثبت شده است";
                return View();
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
            var teachers = _context.Instructors.ToList();
            ViewBag.teachers = teachers;
            return View(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTeacher(int classId, int teacherId)
        {
            Console.WriteLine($"===================== {classId} = {teacherId} =======================");
            var classItem = await _context.Sections.FirstOrDefaultAsync(c=>c.SectionId==classId);

            var instructor = await _context.Instructors.FirstOrDefaultAsync(i=>i.Instructor_Id==teacherId);
            var teachers = _context.Instructors.ToList();
            ViewBag.teachers = teachers;
            
            if (classItem == null)
            {
                ViewBag.ErrorMessage = "کلاس پیدا نشد";
                return View(teachers);
            }
            if (instructor == null)
            {
                ViewBag.ErrorMessage = "استاد پیدا نشد";
                return View(teachers);
            }
            var cours = _context.Courses.Find(classItem.Course_Id);
            if (cours == null)
            {
                ViewBag.ErrorMessage = "درس پیدا نشد";
                return View(teachers);
            }

            bool hasvalid = _context.Teaches.Any(t => t.Section_Id == classItem.Id);

            if (hasvalid)
            {
                ViewBag.ErrorMessage = "این کلاس استاد دارد";
                return View(teachers);
            }
            if (cours.Department_Id != instructor.Department_Id)
            {
                ViewBag.ErrorMessage = "استاد مال دانشکده دیگری است";
                return View(teachers);
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
            var teaches = _context.Teaches.FirstOrDefault(t => t.Section_Id == section.Id);

            if(teaches == null )
            {
                ViewBag.ErrorMessage = "این کلاس استاد ندارد";
                return View();
            }
            var instructor = _context.Instructors.FirstOrDefault(i=>i.Id==teaches.Instructor_Id);
            if (instructor == null)
            {
                ViewBag.ErrorMessage = "این کلاس استاد ندارد";
                return View();
            }
            if (instructor.User_Id == student.User_Id)
            {
                ViewBag.ErrorMessage = "استاد نمی تواند دانشجوی خود باشد";
                return View();
            }
            if (section.Capacity == 0)
            {
                ViewBag.ErrorMessage = "ظرفیت کلاس تکمیل است";
                return View();
            }
            if (cours.Prerequisite != 0)
            {
                var CourseSection = await _context.Sections.FirstOrDefaultAsync(s=>s.Course_Id==cours.Prerequisite);
                if (CourseSection == null)
                {
                    ViewBag.ErrorMessage = "دانشجو پیشنیاز این درس را پاس نکرده است";
                    return View();
                }
                var StudentTakeSection = await _context.Takes.FirstOrDefaultAsync(t=>t.Student_Id==student.Id && t.Section_Id==CourseSection.Id);
                if (StudentTakeSection==null)
                {
                    ViewBag.ErrorMessage = "دانشجو پیشنیاز این درس را پاس نکرده است";
                    return View();
                }
                if (double.Parse(StudentTakeSection.Grade) <12)
                {
                    ViewBag.ErrorMessage = "دانشجو پیشنیاز این درس را پاس نکرده است";
                    return View();
                }
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
                section.Capacity--;
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
            if (classItem == null)
            {
                ViewBag.ErrorMessage = "کلاس یافت نشد";
                return View();
            }
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
            if (student == null)
            {
                ViewBag.ErrorMessage = "دانشجوی مورد نظر یاف نشد";
                return View();
            }
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.SectionId == classId);
            if (section == null)
            {
                ViewBag.ErrorMessage = "کلاس مورد نظر یافت نشد";
                return View();
            }
            var record = await _context.Takes.FirstOrDefaultAsync(t=> t.Section_Id==section.Id && t.Student_Id==student.Id);
            if (record != null)
            {
                _context.Takes.Remove(record);
                section.Capacity++;
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
                var section =await _context.Sections.FirstOrDefaultAsync(s => s.Course_Id == course.Id);
                if(section != null)
                {
                    await DeleteClass(section.SectionId);
                }
                
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
            if (User == null)
            {
                ViewBag.ErrorMessage = "کاربر یافت نشد";
                return View();
            }
            if (User != null)
            {
                var students =await _context.Students.Where(s => s.User_Id == User.Id).ToListAsync();
                try
                {
                    if (students.Any())
                    {
                        foreach (var i in students)
                        {
                            await DeleteStudent(i.Student_Id);
                        }
                        _context.Students.RemoveRange(students);
                    }
                    var instructor = await _context.Instructors.Where(s => s.User_Id == User.Id).ToListAsync();

                    if (instructor.Any())
                    {
                        foreach (var i in instructor)
                        {
                            await DeleteTeacher(i.Instructor_Id);
                        }
                        _context.Instructors.RemoveRange(instructor);
                    }
                }
                catch 
                {
                    ViewBag.ErrorMessage = "لطفا دوباره امتحان کنید";
                    return View();
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
                var teaches =await _context.Teaches.Where(s => s.Section_Id ==classItem.Id).ToListAsync();

                if (teaches.Any())
                {
                    _context.Teaches.RemoveRange(teaches);
                }
                var takes =await _context.Takes.Where(s => s.Section_Id == classItem.Id).ToListAsync();

                if (takes.Any())
                {
                    _context.Takes.RemoveRange(takes);
                }
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
                var teaches = _context.Teaches.Where(s => s.Instructor_Id == teacher.Id).ToList();

                if (teaches.Any())
                {
                    _context.Teaches.RemoveRange(teaches);
                }
                var department = _context.Departments.Find(teacher.Department_Id);
                department.Budget += teacher.Salary;
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
                var takes = _context.Takes.Where(s => s.Student_Id == Student.Id).ToList();

                if (takes.Any())
                {
                    _context.Takes.RemoveRange(takes);
                }
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}

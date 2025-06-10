using Microsoft.AspNetCore.Mvc;
using Golestan.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Golestan.Data;

namespace Golestan.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard(int studentId)
        {
            
            var courses = _context.Takes
                .Include(t => t.sections)
                    .ThenInclude(s => s.courses)
                .Include(t => t.sections)
                    .ThenInclude(s => s.teaches)
                        .ThenInclude(te => te.instructors)
                .Where(t => t.Student_Id == studentId)
                .ToList();

            return View(courses);
        }

     
        public IActionResult CourseDetails(int takeId)
        {
            var take = _context.Takes
                .Include(t => t.sections)
                    .ThenInclude(s => s.courses)
                .Include(t => t.sections)
                    .ThenInclude(s => s.teaches)
                        .ThenInclude(te => te.instructors)
                .FirstOrDefault(t => t.Student_Id == takeId);

            if (take == null) return NotFound();

            return View(take);
        }

   
        public IActionResult Grades(int studentId)
        {
            var grades = _context.Takes
                .Include(t => t.sections)
                    .ThenInclude(s => s.courses)
                .Where(t => t.Student_Id == studentId)
                .Select(t => new {
                    CourseTitle = t.sections.courses.Title,
                    Grade = t.Grade
                })
                .ToList();

            return View(grades);
        }

    
        public IActionResult Profile(int studentId)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null) return NotFound();

           
            var grades = _context.Takes
                .Where(t => t.Student_Id == studentId)
                .ToList();

            double gpa = 0;
            int count = 0;

            foreach (var grade in grades)
            {
                if (double.TryParse(grade.Grade, out double val))
                {
                    gpa += val;
                    count++;
                }
            }
            gpa = count > 0 ? gpa / count : 0;

            ViewBag.GPA = gpa;
            ViewBag.PassedCoursesCount = grades.Count(g => double.TryParse(g.Grade, out double v) && v >= 10);

            return View(student);
        }

       
        [HttpPost]
        public IActionResult CancelAssignment(int takeId)
        {
            var take = _context.Takes.Find(takeId);
            if (take == null) return NotFound();

            _context.Takes.Remove(take);
            _context.SaveChanges();

            return RedirectToAction("MyCourses", new { studentId = take.Student_Id });
        }
    }
}

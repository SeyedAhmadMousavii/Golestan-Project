using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class TeacherController : Controller
{
    private readonly AppDbContext _context;

    public TeacherController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Dashboard(int id)
    {
        var teacher = _context.Instructors.FirstOrDefault(t => t.User_Id == id);

        var sectionIds = _context.Teaches
            .Where(t => t.Instructor_Id == teacher.Id)
            .Select(t => t.Section_Id)
            .ToList();

        var sections = _context.Sections
            .Where(s => sectionIds.Contains(s.Id))
            .ToList();

        return View(sections);
    }

    [HttpPost]
    public async Task<IActionResult> Dashboard(int id, string name)
    {
        return RedirectToAction("ClassDetails", "Teacher", new { id = id });
    }

    [HttpGet]
    public async Task<IActionResult> ClassDetails(int id)
    {
        var section = await _context.Sections.FindAsync(id);
        if (section == null)
        {
            return NotFound();
        }

        var cours = await _context.Courses.FindAsync(section.Course_Id);

        var take = await _context.Takes
            .Where(t => t.Section_Id == section.Id)
            .ToListAsync();

        var students = await _context.Students
            .Include(s => s.User)
            .Where(s => s.takes.Any(t => t.Section_Id == section.Id))
            .ToListAsync();

        var user = await _context.Users
            .Where(u => u.students.Any(s => s.takes.Any(t => t.Section_Id == section.Id)))
            .ToListAsync();

        return View(new SectionDetailsViewModel
        {
            Users = user,
            Section = section,
            courses = cours,
            Students = students,
            Takes = take
        });
    }

    [HttpPost]
    public async Task<IActionResult> SubmitGrade(int StudentId, int SectionId, string Grade)
    {
        try
        {
           
            if (string.IsNullOrWhiteSpace(Grade))
            {
                TempData["Error"] = "نمره نمی‌تواند خالی باشد";
                return RedirectToAction("ClassDetails", new { id = SectionId });
            }

          
            var take = await _context.Takes
                .FirstOrDefaultAsync(t => t.Student_Id == StudentId && t.Section_Id == SectionId);

            if (take != null)
            {
                
                take.Grade = Grade;
            }
            else
            {
               
                _context.Takes.Add(new Takes
                {
                    Student_Id = StudentId,
                    Section_Id = SectionId,
                    Grade = Grade
                });
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "نمره با موفقیت ثبت شد";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"خطا در ثبت نمره: {ex.Message}";
        }

        return RedirectToAction("ClassDetails", new { id = SectionId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveStudent(int StudentId, int SectionId)
    {
        try
        {
           
            var take = await _context.Takes
                .FirstOrDefaultAsync(t => t.Student_Id == StudentId && t.Section_Id == SectionId);

            if (take == null)
            {
                TempData["Error"] = "رکورد مورد نظر یافت نشد";
                return RedirectToAction("ClassDetails", new { id = SectionId });
            }

           
            _context.Takes.Remove(take);
            await _context.SaveChangesAsync();

            TempData["Success"] = "دانشجو با موفقیت حذف شد";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"خطا در حذف دانشجو: {ex.Message}";
        }

        return RedirectToAction("ClassDetails", new { id = SectionId });
    }
}
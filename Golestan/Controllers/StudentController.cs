using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard(int id)
    {
        var student = _context.Students.FirstOrDefault(t => t.User_Id == id);
        if (student==null)
        {
            ViewBag.ErrorMessage = "دانشجوی مورد نظر یاف نشد";
            return RedirectToAction("Login", "Account");
        }
        var takes = await _context.Takes
            .Where(t => t.Student_Id == student.Id)
            .Include(t => t.sections)
            .ThenInclude(s => s.courses)
            .Include(t => t.sections.classrooms)
            .Include(t => t.sections.time_slots)
            .Include(t => t.sections.teaches)
            .ThenInclude(t => t.instructors) 
            .ToListAsync();

        var courseViewModels = takes.Select(t => new StudentCourseViewModel
        {
            StudentId = t.Student_Id,
            SectionId = t.Section_Id,
            CourseTitle = t.sections.courses.Title,
            CourseCode = t.sections.courses.Code,
            Unit = t.sections.courses.Unit,
            FinalExamDate = t.sections.courses.Final_Exam_Date,
            ClassroomID = t.sections.classrooms.Id,
            TimeSlot = _context.Time_Slots.FirstOrDefault(tt => tt.Id == _context.Sections.FirstOrDefault(s => s.Id == t.Section_Id).Time_Slot_Id).Day+" "+
            + _context.Time_Slots.FirstOrDefault(tt => tt.Id == _context.Sections.FirstOrDefault(s => s.Id == t.Section_Id).Time_Slot_Id).Start_Time.Hour+":"+
            + _context.Time_Slots.FirstOrDefault(tt => tt.Id == _context.Sections.FirstOrDefault(s => s.Id == t.Section_Id).Time_Slot_Id).Start_Time.Minute+"-"+
            + _context.Time_Slots.FirstOrDefault(tt => tt.Id == _context.Sections.FirstOrDefault(s => s.Id == t.Section_Id).Time_Slot_Id).End_Time.Hour+":"+
            + _context.Time_Slots.FirstOrDefault(tt => tt.Id == _context.Sections.FirstOrDefault(s => s.Id == t.Section_Id).Time_Slot_Id).End_Time.Minute,
            InstructorName = _context.Users.FirstOrDefault(u => u.Id == _context.Instructors.FirstOrDefault(i => i.teaches.Any(a => a.Section_Id == t.Section_Id)).User_Id).First_name + " " + _context.Users.FirstOrDefault(u => u.Id == _context.Instructors.FirstOrDefault(i => i.teaches.Any(a => a.Section_Id == t.Section_Id)).User_Id).Last_name,
            //t.sections.teaches?.instructors?.User?.Last_name ?? "نامعلوم",
            Grade = t.Grade,
            IsPassed = double.TryParse(t.Grade,out double g) && g >= 12
        }).ToList();

       
        double totalPoints = 0;
        int totalUnits = 0;


        foreach(var i in courseViewModels)
        {
            totalPoints+= double.Parse(i.Grade)*int.Parse(i.Unit);
            totalUnits += int.Parse(i.Unit);
        }

        double gpa = totalUnits > 0 ? totalPoints / totalUnits : 0;

        var viewModel = new StudentCoursesPageViewModel
        {
            Courses = courseViewModels,
            GPA = gpa
        };

        return View(viewModel);
    }

    private bool TryConvertGradeToPoint(string grade, out double point)
    {
        point = 0;
        if (string.IsNullOrEmpty(grade)) return false;
        switch (grade.ToUpper())
        {
            case "A": point = 4; return true;
            case "B": point = 3; return true;
            case "C": point = 2; return true;
            case "D": point = 1; return true;
            case "F": point = 0; return true;
            default: return false;
        }
    }

    // حذف تخصیص کلاس
    [HttpPost]
    public async Task<IActionResult> Dashboard(int id, int sectionId)
    {
        var studen = await _context.Students.FirstOrDefaultAsync(s=>s.Id==id);
        var take = await _context.Takes.FirstOrDefaultAsync(t => t.Student_Id == id && t.Section_Id == sectionId);
        if (take != null)
        {
            _context.Takes.Remove(take);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Dashboard", new { studen.User_Id });

    }
}

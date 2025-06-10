using Golestan.Data;
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
    public async Task<IActionResult> Dashboard(int studentId)
    {
        var student = _context.Students.FirstOrDefault(t => t.User_Id == studentId);
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
            SectionId = t.Section_Id,
            CourseTitle = t.sections.courses.Title,
            CourseCode = t.sections.courses.Code,
            Unit = t.sections.courses.Unit,
            FinalExamDate = t.sections.courses.Final_Exam_Date,
            ClassroomID = t.sections.classrooms.Id,
            InstructorName = t.sections.teaches?.instructors?.User?.Last_name ?? "نامعلوم",
            Grade = t.Grade
        }).ToList();

       
        double totalPoints = 0;
        int totalUnits = 0;

        foreach (var course in courseViewModels)
        {
            if (TryConvertGradeToPoint(course.Grade, out double point))
            {
                int.TryParse(course.Unit, out int unit);
                totalPoints += point * unit;
                totalUnits += unit;
            }
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
    public async Task<IActionResult> RemoveCourse(int studentId, int sectionId)
    {
        var take = await _context.Takes.FirstOrDefaultAsync(t => t.Student_Id == studentId && t.Section_Id == sectionId);
        if (take != null)
        {
            _context.Takes.Remove(take);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Dashboard", new { studentId });
    }
}

using Golestan.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TeacherController : Controller
{
    private readonly AppDbContext _context;

    public TeacherController(AppDbContext context)
    {
        _context = context;
    }

    // نمایش لیست کلاس‌های استاد
    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        int teacherId = Convert.ToInt32(HttpContext.Session.GetString("TeacherId"));

        var classes = await _context.Teaches
            .Where(s => s.Instructor_Id == teacherId)
            .Include(s => s.sections)
            .ToListAsync();

        return View("ClassList", classes);
    }

}

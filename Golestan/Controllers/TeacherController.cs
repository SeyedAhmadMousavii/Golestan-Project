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
    public async Task<IActionResult> ClassList()
    {
        int teacherId = Convert.ToInt32(HttpContext.Session.GetString("TeacherId"));

        var classes = await _context.Sections
            .Where(s => s.Instructor_Id == teacherId)
            .Include(s => s.courses)
            .ToListAsync();

        return View("ClassList", classes);
    }

}

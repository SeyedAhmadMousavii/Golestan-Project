using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class TeacherController : Controller
{
    private readonly AppDbContext _context;

    public TeacherController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard(int teacherId)
    {
        var sections = _context.Sections
            .Include(s => s.courses)
            .Include(s => s.teaches)
                .ThenInclude(t => t.instructors)
            .Where(s => s.teaches.Instructor_Id == teacherId)
            .ToList();

        return View(sections);
    }
}

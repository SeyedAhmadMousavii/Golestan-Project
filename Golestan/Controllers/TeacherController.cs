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
    [HttpGet]
    public IActionResult Dashboard(int id)//
    {
        var teacher = _context.Instructors.FirstOrDefault(t => t.User_Id == id);


        //var sections = _context.Sections
        //    .Include(s => s.courses)
        //    .Include(s => s.teaches)
        //    .ThenInclude(t => t.instructors)
        //    .Where(s => s.teaches.Instructor_Id == id)
        //    .ToList();


        var sectionIds = _context.Teaches
        .Where(t => t.Instructor_Id == teacher.Id)
        .Select(t => t.Section_Id)
        .ToList();

        var sections = _context.Sections
        .Where(s => sectionIds.Contains(s.Id))
        .ToList();

        return View(sections);//
    }
}

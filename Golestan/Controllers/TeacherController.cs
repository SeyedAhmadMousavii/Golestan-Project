using Golestan.Data;
using Golestan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq;

public class TeacherController : Controller
{
    private readonly AppDbContext _context;

    public TeacherController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public  IActionResult Dashboard(int id)//
    {
        var teacher = _context.Instructors.FirstOrDefault(t => t.User_Id == id);
        

        var sectionIds = _context.Teaches
        .Where(t => t.Instructor_Id == teacher.Id)
        .Select(t => t.Section_Id)
        .ToList();

        var sections = _context.Sections
        .Where(s => sectionIds.Contains(s.Id))
        .ToList();

        return View(sections);//
    }
    [HttpPost]
    public async Task<IActionResult> Dashboard(int id,string name)
    {
        return RedirectToAction("ClassDetails","Teacher",id);
    }

    [HttpGet]
    public async  Task<IActionResult> ClassDetails(int id)
    {
        Console.WriteLine("\n---------->"+id+"\n");

        var section = await _context.Sections.FindAsync(id);

        var cours = _context.Courses.Find(section.Course_Id);

        var take = _context.Takes.Where(t=>t.Section_Id==section.Id).ToList();

        var students = _context.Students
        .Where(s => s.takes.Any(t => t.Section_Id == section.Id))
        .ToList();

        var user = _context.Users.Where(u => u.students.Any(s => s.takes.Any(t => t.Section_Id == section.Id))).ToList();

        return View(new SectionDetailsViewModel 
        {
            Users=user,
            Section=section,
            courses=cours,
            Students=students,
            Takes=take,
        });
    }
    [HttpPost]
    public async Task<IActionResult> ClassDetails()
    {
        return View();
    }
}

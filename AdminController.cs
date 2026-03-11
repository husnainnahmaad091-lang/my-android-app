using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyBuddyBoss.Core.Data;
using StudyBuddyBoss.Core.Models;

namespace StudyBuddyBoss.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly StudyBuddyContext _context;

    public AdminController(StudyBuddyContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalStudents = await _context.Students.CountAsync();
        var totalHomework = await _context.HomeworkRecords.CountAsync();
        var totalAttendance = await _context.AttendanceRecords.CountAsync();
        var totalMarks = await _context.MarksRecords.CountAsync();

        var vm = new
        {
            TotalStudents = totalStudents,
            TotalHomework = totalHomework,
            TotalAttendance = totalAttendance,
            TotalMarks = totalMarks
        };

        return View(vm);
    }

    [HttpGet]
    public IActionResult AddStudent() => View();

    [HttpPost]
    public async Task<IActionResult> AddStudent(Student student)
    {
        if (!ModelState.IsValid) return View(student);
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Similar endpoints can be added for homework, marks, attendance.
}

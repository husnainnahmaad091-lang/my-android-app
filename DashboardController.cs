using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyBuddyBoss.Core.Data;

namespace StudyBuddyBoss.Web.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly StudyBuddyContext _context;

    public DashboardController(StudyBuddyContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // For now, assume single demo student (id = 1)
        var student = await _context.Students.FirstOrDefaultAsync() ?? new Core.Models.Student
        {
            FullName = "Demo Student",
            Username = "demo"
        };

        var studentId = student.Id;

        var pendingTasks = await _context.HomeworkRecords
            .Where(h => h.StudentId == studentId && !h.IsCompleted)
            .CountAsync();

        var totalAttendance = await _context.AttendanceRecords
            .Where(a => a.StudentId == studentId)
            .ToListAsync();
        var attendancePercent = totalAttendance.Count == 0
            ? 0
            : (int)(100.0 * totalAttendance.Count(a => a.IsPresent) / totalAttendance.Count);

        var today = DateTime.UtcNow.Date;
        var streak = 0;
        var dateCursor = today;
        while (await _context.AttendanceRecords.AnyAsync(a => a.StudentId == studentId && a.Date == dateCursor && a.IsPresent))
        {
            streak++;
            dateCursor = dateCursor.AddDays(-1);
        }

        var vm = new
        {
            StudentName = student.FullName,
            PendingTasks = pendingTasks,
            AttendancePercent = attendancePercent,
            StreakDays = streak
        };

        return View(vm);
    }
}

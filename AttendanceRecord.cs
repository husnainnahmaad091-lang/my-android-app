namespace StudyBuddyBoss.Core.Models;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
    public Student? Student { get; set; }
}

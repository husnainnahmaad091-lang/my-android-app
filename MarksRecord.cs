namespace StudyBuddyBoss.Core.Models;

public class MarksRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public double Score { get; set; }
    public double MaxScore { get; set; }
    public DateTime Date { get; set; }
    public Student? Student { get; set; }
}

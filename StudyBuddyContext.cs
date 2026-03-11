using Microsoft.EntityFrameworkCore;
using StudyBuddyBoss.Core.Models;

namespace StudyBuddyBoss.Core.Data;

public class StudyBuddyContext : DbContext
{
    public StudyBuddyContext(DbContextOptions<StudyBuddyContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<HomeworkRecord> HomeworkRecords => Set<HomeworkRecord>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<MarksRecord> MarksRecords => Set<MarksRecord>();
    public DbSet<UserLogin> UserLogins => Set<UserLogin>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Username)
            .IsUnique();

        modelBuilder.Entity<UserLogin>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}

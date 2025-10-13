using ApiInescafe.Models;

namespace CafeAPiModels;

public class CourseEnrolled
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public required string UserId { get; set; }
    public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; }

    public Course? Course { get; set; }
    public User? User { get; set; }
}
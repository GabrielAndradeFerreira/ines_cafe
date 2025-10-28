namespace ApiInescafe.Models;

public class CourseEnrolledModel
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public DateTime EnrolledAt { get; set; }
}
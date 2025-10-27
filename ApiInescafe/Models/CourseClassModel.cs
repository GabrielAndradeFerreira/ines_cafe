namespace ApiInescafe.Models;

public class CourseClassModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;
    public string Duration { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
}
namespace ApiInescafe.Models;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public List<Review> Reviews { get; set; } = new();
    public List<CourseClass> Classes { get; set; } = new();
} 
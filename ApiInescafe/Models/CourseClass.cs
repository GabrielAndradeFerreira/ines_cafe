using ApiInescafe.Models;

namespace CafeAPiModels;

public class CourseClass
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public required string Name { get; set; }
    public required string VideoUrl { get; set; }
    public int DurationInMinutes { get; set; }

    public Course? Course { get; set; }
}
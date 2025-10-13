using ApiInescafe.Models;
using CafeAPiModels;

public class Review
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string Comment { get; set; }
    public EnumGrade Grade { get; set; }
    public int? ProductId { get; set; }
    public int? CourseId { get; set; }
    public Product? Product { get; set; }
    public Course? Course { get; set; }
}
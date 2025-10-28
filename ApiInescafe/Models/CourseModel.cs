using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiInescafe.Models;

public class CourseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<CourseClassModel> Classes { get; set; } = new List<CourseClassModel>();
    [JsonIgnore]
    public ICollection<CourseEnrolledModel> Enrolled { get; set; } = new List<CourseEnrolledModel>();
    [JsonIgnore]
    public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
}
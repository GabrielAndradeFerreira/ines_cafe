namespace ApiInescafe.Models;

public class ReviewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public ProductModel Product { get; set; } = null!;
    public int CourseId { get; set; }
    public CourseModel Course { get; set; } = null!;
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public string Comment { get; set; } = string.Empty;
    public RatingEnum Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
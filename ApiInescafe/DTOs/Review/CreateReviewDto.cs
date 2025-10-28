using ApiInescafe.Models;

namespace ApiInescafe.DTOs.Review;

public class CreateReviewDto
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public RatingEnum Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
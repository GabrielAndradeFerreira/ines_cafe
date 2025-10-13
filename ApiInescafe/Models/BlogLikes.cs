namespace ApiInescafe.Models;

public class BlogLikes
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public required string UserId { get; set; }
    public DateTime LikedDate { get; set; } = DateTime.UtcNow;
    public Blog? Blog { get; set; }
    public User? User { get; set; }
}
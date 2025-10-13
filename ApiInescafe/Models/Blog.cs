using ApiInescafe.Models;

namespace CafeAPiModels;

public class Blog
{
    public int Id { get; set; }
    public required string UserId { get; set; } // Foreign Key
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Content { get; set; }
    public required string ImageUrl { get; set; }
    public DateTime PublishedDate { get; set; }
    public User? User { get; set; }
    public List<BlogLikes> Likes { get; set; } = new();
    public bool IsLiked { get; set; }
    public int LikesCount => Likes.Count;
}
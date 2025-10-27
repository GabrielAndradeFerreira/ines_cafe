using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiInescafe.Models;
public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int AuthorId { get; set; }
    public UserModel Author { get; set; } = null!;
    [JsonIgnore]
    public ICollection<blogLikesModel> Likes { get; set; } = new List<blogLikesModel>();

    [NotMapped]
    public int LikeCount => Likes.Count();
}

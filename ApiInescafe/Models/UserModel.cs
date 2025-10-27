using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ApiInescafe.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    [JsonIgnore]
    public ICollection<BlogModel> BlogPosts { get; set; } = new List<BlogModel>();
    [JsonIgnore]
    public ICollection<blogLikesModel> BlogLikes { get; set; } = new List<blogLikesModel>();
    [JsonIgnore]
    public ICollection<CourseModel> Course { get; set; } = new List<CourseModel>();
    [JsonIgnore]
    public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
}
using System.Text.Json.Serialization;
using ApiInescafe.Enums;
using Microsoft.AspNetCore.Identity;

namespace ApiInescafe.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CompleteName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
    public GenderEnum Gender { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
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
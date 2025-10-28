namespace ApiInescafe.Models;

public class blogLikesModel
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public BlogModel Blog { get; set; } = null!;
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
}
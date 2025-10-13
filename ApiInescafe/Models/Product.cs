namespace ApiInescafe.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public int Stock { get; set; }
    public List<Review> Reviews { get; set; } = new();

}

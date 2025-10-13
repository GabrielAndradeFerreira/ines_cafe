using ApiInescafe.Models;

namespace CafeAPiModels;

public class SignaturePlan
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public required string MainColor { get; set; }
}
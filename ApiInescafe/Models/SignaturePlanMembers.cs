namespace ApiInescafe.Models;

public class SignaturePlanMembers
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public DateTime ExpirationDate { get; set; } 
}
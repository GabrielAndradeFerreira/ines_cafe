namespace ApiInescafe.Models;

public class UserSubscription
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public int SignaturePlanId { get; set; }
    public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;
    public DateTime ExpirationDate { get; set; }

    public User? User { get; set; }
    public SignaturePlan? SignaturePlan { get; set; }
}
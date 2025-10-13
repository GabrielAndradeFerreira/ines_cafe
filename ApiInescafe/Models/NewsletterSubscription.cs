namespace ApiInescafe.Models;

public class NewsletterSubscription
{
    public int Id { get; set; }
    public required string UserId { get; set; }

    public User? User { get; set; }
}
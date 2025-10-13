using System.ComponentModel.DataAnnotations;

namespace CafeAPiModels;

public class NewsletterContact
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? Topic { get; set; }

    [Required]
    [StringLength(2000)]
    public string? Message { get; set; }

    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}
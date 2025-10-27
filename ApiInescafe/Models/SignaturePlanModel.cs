using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiInescafe.Models;

public class SignaturePlanModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<SignaturePlanMembersModel> Members { get; set; } = new List<SignaturePlanMembersModel>();
}
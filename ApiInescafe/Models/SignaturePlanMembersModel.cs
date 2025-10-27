namespace ApiInescafe.Models;

public class SignaturePlanMembersModel
{
    public int Id { get; set; }
    public int SignaturePlanId { get; set; }
    public SignaturePlanModel SignaturePlan { get; set; } = null!;
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
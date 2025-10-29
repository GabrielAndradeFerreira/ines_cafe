using ApiInescafe.Enums;

namespace ApiInescafe.DTOs;

public class PersonalInfoDto
{
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string CompleteName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
    public GenderEnum Gender { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ApiInescafe.DTOs;

public class NewsletterDTO
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public required string Email { get; set; }
    }